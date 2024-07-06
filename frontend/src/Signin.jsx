

function Signin(){


    return(
        <div className="sign-container">
            <div className="sign-box">
                <span>Name: </span>
                <input className="name"></input>
                <br/>
                <span>Surname: </span>
                <input className="surname"></input>
                <br/>
                <span>Email: </span>
                <input className="email"></input>
                <br/>
                <span>Password: </span>
                <input className="password"></input>
                <br/>
                <span>Password Again: </span>
                <input className="password"></input>
                <br/>
                <button className="sign-button">Sign in</button>
            </div>
        </div>
    );
}

export default Signin