import { Switch ,FormControlLabel } from "@mui/material";
import { styled } from "@mui/system";
import LockOpenIcon from '@mui/icons-material/LockOpen';

interface TwoFactorAuthenticationButtonProps {
        twoFactorAuthentication: boolean;
        setTwoFactorAuthentication: (twoFactorAuthentication: boolean) => void;
}

const TwoFactorAuthenticationButton: React.FC<TwoFactorAuthenticationButtonProps> = ({... rest}) => {
    return (
        <FormControlLabel
                control={
                        <Switch
                                checked={rest.twoFactorAuthentication}
                                onChange={() => rest.setTwoFactorAuthentication(!rest.twoFactorAuthentication)}
                                color="primary"
                        />
                }
                label="Two Factor Authentication"
                className="dark:text-white text-black"
                labelPlacement="start"
        />
    )
}

export default TwoFactorAuthenticationButton