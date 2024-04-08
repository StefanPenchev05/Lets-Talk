import { api } from "@services/api";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import Loader from "src/pages/Loader";

function index() {
  const { token } = useParams();

  useEffect(() => {
    const sendVerify = async () => {
      try {
        await api(`/auth/register/verify?token=${token}`, {
          method: "GET",
        }).then(() => {
          window.close();
        });
      } catch (err) {
        console.log(err);
      }
    };

    sendVerify();
  });

  return <Loader />;
}

export default index;
