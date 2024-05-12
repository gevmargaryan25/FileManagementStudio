import { useNavigate } from "react-router-dom";
import React, { useState, useEffect } from 'react';


const UserProfile: React.FC = () => {

    const navigate = useNavigate();
    const [files, setFiles] = useState([]);

    useEffect(() => {
        populateFilesTable();
    }, []);

    // Function to fetch file entities from the server and populate the table
    const populateFilesTable = async () => {
        const token = sessionStorage.getItem('token');
        try {
            const response = await fetch("/api/Storage/Get", {
                method: "GET",
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            if (response.ok) {
                const filesData = await response.json();
                setFiles(filesData);
            } else {
                console.error("Failed to fetch files:", response.statusText);
                // Handle error, if needed
            }
        } catch (error) {
            console.error("Error fetching files:", error);
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
            const token = sessionStorage.getItem('token');
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
                    populateFilesTable(); 
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
                sessionStorage.removeItem('token');
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

    const handleFileDelete = async (fileName: string) => {
        const token = sessionStorage.getItem('token');
        try {
            const response = await fetch(`/api/Storage/${fileName}`, {
                method: "DELETE",
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            if (response.ok) {
                console.log("File deleted successfully");
                populateFilesTable(); // Refresh the table after successful deletion
                // Handle success, if needed
            } else {
                console.error("Failed to delete file:", response.statusText);
                // Handle error, if needed
            }
        } catch (error) {
            console.error("Error deleting file:", error);
            // Handle error, if needed
        }
    };

    return (
        <div>
            <h2>User Profile Page</h2>
            <button onClick={handleLogout}>Logout</button>
           
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

            <h2>Files Table</h2>
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Extension</th>
                        <th>Size (Bytes)</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {files.map((file) => (
                        <tr key={file.name}>
                            <td>{file.name}</td>
                            <td>{file.type}</td>
                            <td>{file.size}</td>
                            <td>
                                <button onClick={() => handleFileDelete(file.name)}>
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default UserProfile;