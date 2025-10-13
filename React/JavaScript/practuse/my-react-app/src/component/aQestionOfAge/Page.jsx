import React from "react";
import { useState } from "react";

function Page() {

    const [name, setName] = useState("");
    const [submitted, setSubitted]  = useState(false);

    const handleSubmit = (e) => {
        e.preventDefault();
        setSubitted(true);
    }

    return (
        <div>
            <h1>Hello</h1>
            {!submitted ? (
                <form onSubmit={handleSubmit}>
                    <label>
                        What is your name?
                        <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        />
                    </label>
                    <button type="submit">Submit</button>
                </form>
            ) : (
                <p>you are {name} years old.</p>
            )}
        </div>
        
    
    );
}
export default Page;