// const MyHeader: React.FC = () => {
//   return (
//     <div
//       className="mt-3 mb-3 ml-0 mr-0"
//       style={{ maxWidth: "600px", minWidth: "200px", width: "100%" }}
//     >
//       <div className="container text-center">
//         <div className="row">
//           <div className="col">
//             <img className="avatar" src="mira-avatar.webp" alt="Mira" />
//           </div>
//           <div className="col h1">Conversational Friends</div>
//           <div className="col">
//             <img className="avatar" src="pierre-avatar.webp" alt="Pierre" />
//           </div>
//         </div>
//       </div>
//     </div>
//   );
// };

const MyHeader = () => {
  return (
    <header
      className="w-100 d-flex justify-content-center"
      style={{ backgroundColor: "rgba(0, 0, 0, 0.5)", borderRadius: 10 }}
    >
      <div
        className="container d-flex align-items-center justify-content-between"
        style={{ maxWidth: "600px" }}
      >
        {/* Left Avatar - hidden on small screens */}
        <div
          className="d-none d-sm-block"
          style={{ width: "150px", height: "150px" }}
        >
          <img
            src="mira-avatar.webp"
            alt="Mira's avatar"
            className="rounded-circle w-100 h-100"
            style={{ objectFit: "cover" }}
          />
        </div>

        {/* Title - centered and responsive */}
        <h1 className="fs-4 fw-bold text-white my-3">ConversationalFriends</h1>

        {/* Right Avatar - only visible on desktop */}
        <div
          className="d-none d-sm-block"
          style={{ width: "150px", height: "150px" }}
        >
          <img
            src="pierre-avatar.webp"
            alt="Pierre's avatar"
            className="rounded-circle w-100 h-100"
            style={{ objectFit: "cover" }}
          />
        </div>

        {/* Empty div for spacing on mobile */}
        <div className="d-sm-none" style={{ width: "0" }}></div>
      </div>
    </header>
  );
};

export default MyHeader;
