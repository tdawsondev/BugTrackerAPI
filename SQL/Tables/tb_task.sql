-- Table: public.task

-- DROP TABLE IF EXISTS public.task;

CREATE TABLE IF NOT EXISTS public.task
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    name character varying COLLATE pg_catalog."default" NOT NULL,
    description character varying COLLATE pg_catalog."default",
    assigned_userid uuid,
    status character varying COLLATE pg_catalog."default" NOT NULL,
    estimated_time bigint,
    remaining_time bigint,
    parent_featureid uuid,
    CONSTRAINT task_pkey PRIMARY KEY (id),
    CONSTRAINT parent_feature FOREIGN KEY (parent_featureid)
        REFERENCES public.feature (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.task
    OWNER to postgres;