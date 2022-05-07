-- Table: public.login

-- DROP TABLE IF EXISTS public.login;

CREATE TABLE IF NOT EXISTS public.login
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    user_name character varying(25) COLLATE pg_catalog."default" NOT NULL,
    password character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT login_pkey PRIMARY KEY (id),
    CONSTRAINT user_name UNIQUE (user_name)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.login
    OWNER to postgres;