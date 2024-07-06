-- Create Users table.
CREATE TABLE public.users
(
    id bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name character varying(50) NOT NULL,
    surname character varying(50) NOT NULL,
    email character varying(50) NOT NULL,
    password character varying(72) NOT NULL
);

-- Add dummy data to it.
-- Passwords are: 12345, abcdef
INSERT INTO public.users(name, surname, email, password)
VALUES ('Hava', 'Aydın', 'hava.aydin@gmail.com', '$2a$10$RipXJuXJ8a28Vx6lw0c8ke5OD6gWHkY1SKGUNeApER9JVOOdizJQS'),
VALUES ('Furkan', 'Acar', 'furkan.acar@outlook.com', '$2a$11$iStbUVsLEt3Iu4kTq1iNaeBjYVHAxy5X.So6Dvydn8uX4TPL8XK7G')