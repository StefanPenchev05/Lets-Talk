import React from "react";
import { Divider } from "@mui/material";

type DivierFiledProp = {
  helperText: string;
};

const DividerField: React.FC<DivierFiledProp> = ({ helperText }) => {
  return (
    <Divider
      className="w-full text-gray-400 dark:text-white mb-4"
      sx={{
        ".dark &:before": {
          backgroundColor: "white",
          height: "1px !important",
        },
        ".dark &:after": {
          backgroundColor: "white",
          height: "1px !important",
        },
      }}
    >
      {helperText}
    </Divider>
  );
};

export default DividerField;
