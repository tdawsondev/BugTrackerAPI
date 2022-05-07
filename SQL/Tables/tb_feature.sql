-- Table: public.feature

-- DROP TABLE IF EXISTS public.feature;

CREATE TABLE IF NOT EXISTS public.feature
(
    id uuid NOT NULL DEFAULT uuid_generate_v4(),
    name character varying COLLATE pg_catalog."default" NOT NULL,
    description character varying COLLATE pg_catalog."default",
    status character varying COLLATE pg_catalog."default" NOT NULL,
    sprint_id uuid,
    CONSTRAINT feature_pkey PRIMARY KEY (id),
    CONSTRAINT sprint_id FOREIGN KEY (sprint_id)
        REFERENCES public.sprint (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.feature
    OWNER to postgres;