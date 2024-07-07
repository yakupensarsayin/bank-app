import {SubmitHandler, useForm} from 'react-hook-form'
import { useNavigate } from 'react-router-dom';
import '../index.css'



type LoginFormValues = {
  email: string;
  password: string;
}

function Login() {
  const {register, handleSubmit, formState} = useForm<LoginFormValues>();
  const {errors} = formState;

  const navigate = useNavigate();

  const onSubmit: SubmitHandler<LoginFormValues> = async (formValues) => {

    try {

      const response = await fetch("https://localhost:7130/api/Auth/Login", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(formValues)
      });

      const data = await response.json();

      if (response.ok) {
        console.log('Login successful:', data);
      }
      else {
        console.error('Login failed:', data);
      }

    } catch (error) {
      console.error('Error:', error);
    }
  }

  const handleRegisterClick = () => {
    navigate('/register.html');
  };

  return (
      <div className="log-container">

        <div className="log-box">

          <form onSubmit={handleSubmit(onSubmit)}>

            <label htmlFor="email">Email</label>
            <input id="email" type="email" {...register("email", {
              required: { value: true, message: "Email is required."}, 
              minLength: { value: 3, message: "Email must be more than 3 characters."},
              maxLength: { value: 50, message:  "Email can not exceed 50 characters."}
            })}/>
            <p className="error">{errors.email?.message}</p>

            <label htmlFor="password">Password</label>
            <input id="password" type="password" {...register("password", {
              required: { value: true, message: "Password is required."}, 
              maxLength: { value: 30, message:  "Password can not exceed 30 characters."}
            })}/>
            <p className="error">{errors.password?.message}</p>

            <input id="submit" type="submit"/>
          </form>

          <div className="register-link" onClick={handleRegisterClick}>
            Register
          </div>

        </div>
      </div>
  )
}

export default Login
