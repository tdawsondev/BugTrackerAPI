-- FUNCTION: public.get_project_users(uuid)

-- DROP FUNCTION IF EXISTS public.get_project_users(uuid);

CREATE OR REPLACE FUNCTION public.get_project_users(
	projectid uuid)
    RETURNS TABLE(uid uuid, name character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
RETURN QUERY
SELECT id, user_name
FROM public.login INNER JOIN 
(SELECT user_id  FROM public.project_users  WHERE project_id = projectID ) 
AS foo ON foo.user_id = public.login.id;

end;
$BODY$;

ALTER FUNCTION public.get_project_users(uuid)
    OWNER TO postgres;
