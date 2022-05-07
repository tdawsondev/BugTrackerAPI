-- Table: public.project_users

-- DROP TABLE IF EXISTS public.project_users;

CREATE TABLE IF NOT EXISTS public.project_users
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    project_id uuid NOT NULL,
    user_id uuid NOT NULL,
    CONSTRAINT project_users_pkey PRIMARY KEY (id),
    CONSTRAINT project_id FOREIGN KEY (project_id)
        REFERENCES public.project (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT user_id FOREIGN KEY (user_id)
        REFERENCES public.login (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.project_users
    OWNER to postgres;