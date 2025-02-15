interface MyCardProps {
  children?: React.ReactNode;
  className?: string;
}

const MyCard: React.FC<MyCardProps> = ({ children, className }) => {
  return (
    <div
      className={`card mt-3 mb-3 p-3 ${className}`}
      style={{
        borderRadius: 10,
        boxShadow: "5px 5px 5px rgba(0, 0, 0, 0.3)",
      }}
    >
      {children}
    </div>
  );
};

export default MyCard;
