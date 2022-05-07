-- PROCEDURE: public.create_project(character varying, character varying, uuid)

-- DROP PROCEDURE IF EXISTS public.create_project(character varying, character varying, uuid);

CREATE OR REPLACE PROCEDURE public.create_project(
	IN newname character varying,
	IN des character varying,
	IN createdby uuid)
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE
newId UUID := uuid_generate_v4();
begin

INSERT INTO public.project (id, name, description, created_by_id) VALUES (newId, newName, des, createdBy);

INSERT INTO public.project_users(
	id, project_id, user_id)
	VALUES (default, newId, createdBy);

end;
$BODY$;
