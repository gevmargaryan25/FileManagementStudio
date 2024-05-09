import React from "react";

const UserProfile: React.FC = () => {
    // Function to handle file upload
    const handleFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files && event.target.files.length > 0) {
            const file = event.target.files[0]; // Get the selected file
            console.log("Selected file:", file);
            // Here you can perform further actions such as uploading the file to a server
        }
    };

    return (
        <div>
            <h2>User Profile Page</h2>
            <input
                type="file"
                id="fileInput"
                onChange={handleFileUpload}
                style={{ display: "none" }} // Hide the file input element
            />
            <button
                onClick={() => {
                    const fileInput = document.getElementById(
                        "fileInput"
                    ) as HTMLInputElement; // Type assertion for file input
                    if (fileInput) {
                        fileInput.click(); // Trigger file input click on button click
                    }
                }}
            >
                Upload File
            </button>
        </div>
    );
};

export default UserProfile;