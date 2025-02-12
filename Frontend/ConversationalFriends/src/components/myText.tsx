interface MyTextProps {
  header?: string;
  children?: React.ReactNode;
}

const MyText: React.FC<MyTextProps> = ({ header, children }) => {
  return (
    <div className="mt-3 mb-3" style={{ maxWidth: "600px" }}>
      {header && (
        <p className="h1 text-center" style={{ color: "#222222" }}>
          {header}
        </p>
      )}
      <p>{children}</p>
    </div>
  );
};

export default MyText;
