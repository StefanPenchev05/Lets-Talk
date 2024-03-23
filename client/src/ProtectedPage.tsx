import { ProtectedPage, AuthSessionResponse } from "@types";
import { useNavigate } from "react-router-dom";
import Cookie from "js-cookie";
import { useEffect, useState } from "react";
import { api } from "@services/api";
import Alert from "@components/protected/Alert";

const ProtectedPage: React.FC<ProtectedPage> = ({ children }) => {
    const [showAlert, setShowAlert] = useState(false);
    const [type, setType] = useState<'success' | 'error' | 'warning'>("error");
    const [message, setMessage] = useState("");
    const navigate = useNavigate();

    const isAuthenticated = async () => {
        try {
            const response = await api("http://localhost:5295/auth/session", { method: "GET" }) as { data: AuthSessionResponse };
            const { data } = response;
            if (data.authSession) {
                setShowAlert(true);
                setType("success");
                setMessage(data.message);
                setTimeout(() => {
                    setShowAlert(false);
                }, 10000);
                return true;
            }
            return false;
        } catch (err: any) {
            const response = await api("http://localhost:5295/auth/session", { method: "GET" }) as { data: AuthSessionResponse };
            const { data } = response;
            if (!data.errors.authSession) {
                setShowAlert(true);
                setType("success");
                setMessage(data.message);
                setTimeout(() => {
                    setShowAlert(false);
                }, 10000);
                return false;
            }
        }
    };

    useEffect(() => {
        isAuthenticated().then((auth) => {
            if (!auth) {
                navigate("/login");
            }
        });
    }, [navigate]);

    return (
        <>
            {showAlert && <Alert message={message} type={type} />}
            {children}
        </>
    );
};

export default ProtectedPage;