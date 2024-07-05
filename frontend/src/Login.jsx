

function Login(){


    return(
        <div className="log-container">
            <div className="log-box">
                <span>User Name: </span>
                <input className="username"></input>
                <br/>
                <span>Password: </span>
                <input className="password"></input>
                <br/>
                <button className="log-button">Login</button>
            </div>
        </div>
    );
}

export default Login