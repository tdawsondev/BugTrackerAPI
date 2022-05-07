-- Table: public.sprint

-- DROP TABLE IF EXISTS public.sprint;

CREATE TABLE IF NOT EXISTS public.sprint
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    name character varying COLLATE pg_catalog."default" NOT NULL,
    description character varying COLLATE pg_catalog."default",
    status character varying COLLATE pg_catalog."default" NOT NULL,
    project_id uuid NOT NULL,
    start_date date,
    end_date date,
    CONSTRAINT sprint_pkey PRIMARY KEY (id),
    CONSTRAINT project FOREIGN KEY (project_id)
        REFERENCES public.project (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.sprint
    OWNER to postgres;