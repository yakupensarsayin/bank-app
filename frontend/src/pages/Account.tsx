import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

// for testing purposes
type accountResponse = {
    status: number;
    message: string;
}

function Account() {
    const [data, setData] = useState<accountResponse>({status: 0, message: ''});

    const navigateLogin = useNavigate();

    // this is not the ideal way of doing things.
    // it's here for testing purposes.
    // TODO: Add interceptor.
    const fetchData = async () => {
        try {
            console.log("This is called");
            

            const response = await fetch("https://localhost:7130/api/Account/GetCustomerData", {
                credentials: 'include',
                headers: {
                    'Content-Type': 'application/json'
                },
            });

            if (response.status === 401) {
                
                console.log("Unauthorized for first time.");

                const response2 = await fetch("https://localhost:7130/api/Auth/Refresh", {
                    method: 'POST',
                    credentials: 'include',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                });

                if(response2.status === 401){
                    console.log("Unauthorized for second time.");
                    navigateLogin('/login');
                    return
                }

                const incoming2 = await response2.json();

                console.log("Got validated");
                setData(incoming2);
                return;
            }

            const incoming = await response.json();

            console.log("Got the data with ease.");
            setData(incoming);

        } catch (error) {
            // console.log(error);
            // setData({status: 401, message: "Unauthorized"});
        }
    }

    useEffect(() => {
        fetchData().then();
    }, []);

  return (
    <div>
      <h1>Status: {data.status}</h1>
      <p>Message: {data.message}</p>
      <button onClick={fetchData}>Try again</button>
    </div>
  );
}

export default Account;