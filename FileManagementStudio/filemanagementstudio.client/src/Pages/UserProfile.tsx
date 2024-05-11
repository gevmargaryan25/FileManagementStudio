import React from "react";


const UserProfile: React.FC = () => {
    // Function to handle file upload
    const handleFileUpload = async (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files && event.target.files.length > 0) {
            const file = event.target.files[0]; // Get the selected file
            console.log("Selected file:", file);

            const formData = new FormData();
            formData.append("file", file); // Append the file to FormData
            const token = localStorage.getItem('token');
            try {
                const response = await fetch("/api/Storage/Upload", {
                    method: "POST",
                    body: formData,
                    headers: {
                        'Authorization': `Bearer ${token}`
                    }
                });

                if (response.ok) {
                    console.log("File uploaded successfully");
                    // Handle success, if needed
                } else {
                    console.error("Failed to upload file:", response.statusText);
                    // Handle error, if needed
                }
            } catch (error) {
                console.error("Error uploading file:");
                // Handle error, if needed
            }
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