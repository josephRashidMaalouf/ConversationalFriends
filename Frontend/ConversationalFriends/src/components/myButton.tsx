interface MyButtonProps {
  className?: string;
  onClick?: () => Promise<void>;
  disabled: boolean;
  children?: React.ReactNode;
}

const MyButton: React.FC<MyButtonProps> = ({
  className,
  onClick,
  disabled,
  children,
}) => {
  return (
    <button
      className={`btn mt-3 mb-3 ${className}`}
      onClick={onClick}
      disabled={disabled}
      children={children}
    />
  );
};

export default MyButton;
