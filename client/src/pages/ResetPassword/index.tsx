import * as ResetImports from "./imports";
import * as GlobalImports from "../../globalImports";
import { api } from "@services/api";
import { validatePassword } from "@utils/login-validations/Password";

const RESOLUTION_THRESHOLD = 1022;

function index() {
  const windowWidth = GlobalImports.useWindowResize();

  const [password, setPassword] = ResetImports.useState<string>("");
  const [showPassword, setShowPassword] = ResetImports.useState<boolean>(false);
  const [passwordError, setPasswordError] = ResetImports.useState<string>("");
  const [responseText, setResponseText] = ResetImports.useState<string>("");
  const [countDown, setCountDown] = ResetImports.useState<number>(-1);

  const [confirmPassword, setConfirmPassword] =
    ResetImports.useState<string>("");
  const [confirmPasswordError] = ResetImports.useState<string>("");

  const { token } = ResetImports.useParams();
  const refDialog = ResetImports.useRef<HTMLDialogElement>(null);
  const navigate = ResetImports.useNavigate();

  ResetImports.useEffect(() => {
    if (countDown > 0) {
      setTimeout(() => {
        setCountDown(countDown - 1);
      }, 1000);
    }

    if (countDown === 0) {
      navigate("/");
    }
  }, [countDown]);

  ResetImports.useEffect(() => {
    const checkValidToken = async () => {
      await api(`/password/reset/token/?token=${token}`, { method: "GET" })
        .then(() => {
          return;
        })
        .catch(() => navigate("/login"));
    };

    checkValidToken();
  }, []);

  const handleFormSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      if (password !== confirmPassword) {
        throw new ResetImports.CustomError(
          "Password and ConfirmPassword should be the same!",
          { type: "password" }
        );
      }

      validatePassword(password);

      await api(`/password/reset/verify/?token=${token}`, {
        method: "POST",
        data: JSON.stringify({ confirmPassword }),
      })
        .then((response: any) => {
          const data = response.data as ResetImports.IResetPasswordResponse;
          console.log(data);
          setResponseText(data.message);
          refDialog.current?.showModal();
          setCountDown(10);
        })
        .catch((err: any) => {
          console.log(err);
          const data = err.response.data as ResetImports.IResetPasswordResponse;
          if (data.invalidToken) {
            navigate("/login");
          } else if (
            data.samePassword ||
            data.passwordChanged ||
            data.emptyPassword
          ) {
            setPasswordError(data.message);
          }
        });
    } catch (err: any) {
      if (err instanceof ResetImports.CustomError) {
        if (Object.values(err.details)[0] == "password") {
          setPasswordError(err.message);
          return;
        }
      }
      console.log(err);
    }
  };

  return (
    <div className="flex flex-col md:flex-row items-center justify-center h-screen md:h-[100dvh] w-full">
      <div className="hidden lg:block w-3/4 h-full bg-white dark:bg-[#150f38]">
        <img
          src={ResetImports.Wallpaper}
          alt="Login Wallpaper"
          className="w-full h-full object-cover"
        />
      </div>
      <div className="w-full h-screen lg:w-1/4 flex flex-col items-center justify-center max-md:px-8 md:p-8 loginBox dark:bg-[#040622]">
        <img src={ResetImports.ManImg} alt="Avatar" className="w-1/3 mb-5" />
        {windowWidth <= RESOLUTION_THRESHOLD ? (
          <ResetImports.Subtitle align="center" gutterBottom />
        ) : (
          <ResetImports.Subtitle align="left" gutterBottom />
        )}
        <form
          className="flex flex-col space-y-4 w-full mb-9"
          onSubmit={handleFormSubmit}
          method="post"
        >
          <ResetImports.PasswordInput
            password={password}
            showPassword={showPassword}
            setShowPassword={setShowPassword}
            setPassword={setPassword}
            error={passwordError}
          />

          <ResetImports.ConfirmPassword
            confirmPassword={confirmPassword}
            setConfirmPassword={setConfirmPassword}
            error={confirmPasswordError}
            showConfirmPassword={showPassword}
          />
          <ResetImports.SubmitButton helperText="Reset Password" />
        </form>
        <dialog className="modal" ref={refDialog}>
          <div className="modal-box space-y-8 bg-white dark:bg-neutral-100">
            <p className="font-bold text-lg w-full text-center text-green-500">
              {responseText}
            </p>
            <p className="font-bold text-lg w-full text-center">
              This Dialog will close after:{" "}
            </p>
            <p className="font-bold text-lg w-full text-center">{countDown}</p>
          </div>
        </dialog>
      </div>
    </div>
  );
}

export default index;
