-- Table: public.subtask

-- DROP TABLE IF EXISTS public.subtask;

CREATE TABLE IF NOT EXISTS public.subtask
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    name character varying COLLATE pg_catalog."default" NOT NULL,
    description character varying COLLATE pg_catalog."default",
    assigned_userid uuid,
    status character varying COLLATE pg_catalog."default" NOT NULL,
    estimated_time bigint,
    remaining_time bigint,
    parent_taskid uuid,
    CONSTRAINT parent_task FOREIGN KEY (parent_taskid)
        REFERENCES public.task (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.subtask
    OWNER to postgres;