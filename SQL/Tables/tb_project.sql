-- Table: public.project

-- DROP TABLE IF EXISTS public.project;

CREATE TABLE IF NOT EXISTS public.project
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    name character varying(25) COLLATE pg_catalog."default" NOT NULL,
    description character varying COLLATE pg_catalog."default",
    created_by_id uuid NOT NULL,
    CONSTRAINT project_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.project
    OWNER to postgres;