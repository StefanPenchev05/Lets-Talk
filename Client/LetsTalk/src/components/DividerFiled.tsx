import { Divider } from "@mui/material";
import { ReactNode } from "react";

type DividerFiledProps<ItemType extends ReactNode> = {
  helperText: ItemType;
};

const DividerField: React.FC<DividerFiledProps<ReactNode>> = <ItemType extends ReactNode,>({ helperText }: DividerFiledProps<ItemType>) => {
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
}

export default DividerField;
