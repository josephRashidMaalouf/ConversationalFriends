interface MyInputProps {
  value: string;
  placeholder: string;
  disabled: boolean;
  type: string;
  className?: string;
  onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const MyInput: React.FC<MyInputProps> = ({
  value,
  placeholder,
  disabled,
  type,
  className = "",
  onChange,
}) => {
  return (
    <input
      type={type}
      style={{ maxWidth: "600px" }}
      className={`mt-3 mb-3 ${className} form-control`}
      placeholder={placeholder}
      value={value}
      onChange={onChange}
      disabled={disabled}
    />
  );
};

export default MyInput;
