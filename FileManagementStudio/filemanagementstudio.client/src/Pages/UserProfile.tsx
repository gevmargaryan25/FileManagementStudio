import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";


const UserProfile: React.FC = () => {

    const [fileNames, setFileNames] = useState<string[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        fetchFileNames();
    }, []);

    const fetchFileNames = async () => {
        const token = localStorage.getItem('token');
        try {
            const response = await fetch("/api/Storage/Get", {
                method: "GET",
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const data = await response.json();
                setFileNames(data); // Update file names state
            } else {
                console.error("Failed to fetch file names:", response.statusText);
                // Handle error, if needed
            }
        } catch (error) {
            console.error("Error fetching file names:", error);
            // Handle error, if needed
        }
    };

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

    const handleLogout = async () => {
        try {
            const response = await fetch("/api/account/logout", {
                method: "POST", // Assuming you're using a POST request for logout
                headers: {
                    'Content-Type': 'application/json'
                },
                // You can include a body with any logout data if required
                // body: JSON.stringify({}),
            });

            if (response.ok) {
                // Clear the token from localStorage
                localStorage.removeItem('token');
                // Redirect to the login page
                navigate("/login");
            } else {
                console.error("Failed to logout:", response.statusText);
                // Handle error, if needed
            }
        } catch (error) {
            console.error("Error logging out:", error);
            // Handle error, if needed
        }
    };

    return (
        <div>
            <h2>User Profile Page</h2>
            <button onClick={handleLogout}>Logout</button>
            <h3>Files:</h3>
            <ul>
                {fileNames.map((fileName, index) => (
                    <li key={index}>{fileName}</li>
                ))}
            </ul>
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