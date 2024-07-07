import { SubmitHandler, useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import '../index.css';

type RegisterFormValues = {
  name: string;
  surname: string;
  email: string;
  password: string;
}

function Register() {
  const { register, handleSubmit, formState } = useForm<RegisterFormValues>();
  const { errors } = formState;

  const navigate = useNavigate();

  const onSubmit: SubmitHandler<RegisterFormValues> = async (formValues) => {
    try {
      const response = await fetch("https://localhost:7130/api/Auth/Register", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(formValues)
      });

      const data = await response.json();

      if (response.ok) {
        console.log('Signin successful:', data);
      } else {
        console.error('Signin failed:', data);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  }

  const handleLoginClick = () => {
    navigate('/');
  };

  return (
    <div className="log-container">
      <div className="log-box">
        <form onSubmit={handleSubmit(onSubmit)}>
          <label htmlFor="name">Name</label>
          <input id="name" type="text" {...register("name", {
            required: { value: true, message: "Name is required." },
            minLength: { value: 3, message: "Name must be more than 2 characters." },
            maxLength: { value: 50, message: "Name can not exceed 50 characters." }
          })} />
          <p className="error">{errors.name?.message}</p>

          <label htmlFor="surname">Surname</label>
          <input id="surname" type="text" {...register("surname", {
            required: { value: true, message: "Surname is required." },
            minLength: { value: 3, message: "Surname must be more than 2 characters." },
            maxLength: { value: 50, message: "Surname can not exceed 50 characters." }
          })} />
          <p className="error">{errors.surname?.message}</p>

          <label htmlFor="email">Email</label>
          <input id="email" type="email" {...register("email", {
            required: { value: true, message: "Email is required." },
            minLength: { value: 3, message: "Email must be more than 3 characters." },
            maxLength: { value: 50, message: "Email can not exceed 50 characters." }
          })} />
          <p className="error">{errors.email?.message}</p>

          <label htmlFor="password">Password</label>
          <input id="password" type="password" {...register("password", {
            required: { value: true, message: "Password is required." },
            maxLength: { value: 30, message: "Password can not exceed 30 characters." }
          })} />
          <p className="error">{errors.password?.message}</p>

          <input id="submit" type="submit" />
        </form>

        <div className="login-link" onClick={handleLoginClick}>
          Login
        </div>

      </div>
    </div>
  );
}

export default Register;