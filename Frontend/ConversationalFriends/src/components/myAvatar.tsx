interface MyAvatarProps {
  src: string;
  alt: string;
  size: number;
  avatarName?: string;
}

const MyAvatar: React.FC<MyAvatarProps> = ({ src, alt, size, avatarName }) => {
  return (
    <div
      className="d-flex flex-column align-items-center"
      style={{ width: size, height: size }}
    >
      <img
        src={src}
        alt={alt}
        className="rounded-circle w-100 h-100"
        style={{ objectFit: "cover" }}
      />
      {avatarName && <p className="text-center">{avatarName}</p>}
    </div>
  );
};

export default MyAvatar;
