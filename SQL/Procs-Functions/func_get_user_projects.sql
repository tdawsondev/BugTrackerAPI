-- FUNCTION: public.get_user_projects(uuid)

-- DROP FUNCTION IF EXISTS public.get_user_projects(uuid);

CREATE OR REPLACE FUNCTION public.get_user_projects(
	userid uuid)
    RETURNS TABLE(projectid uuid, projectname character varying, projectdescription character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
RETURN QUERY
SELECT id, name, description
FROM public.project INNER JOIN 
(SELECT project_id  FROM public.project_users  WHERE user_id = userid ) 
AS foo ON foo.project_id = public.project.id;

end;
$BODY$;

ALTER FUNCTION public.get_user_projects(uuid)
    OWNER TO postgres;
