﻿use SASDdb

select 
        st.name [Table],
		sc.column_id,
        sc.name [Column],
		t.name type,
		isnull(p.ORDINAL_POSITION,'') primaryKeyOrder, 
		sc.max_length,
		sc.is_nullable nullable,
        sep.value [Description]
---into abc
    from sys.tables st
    left join sys.columns sc on st.object_id = sc.object_id
	left JOIN 
		sys.types t ON sc.user_type_id = t.user_type_id
    left join sys.extended_properties sep on st.object_id = sep.major_id
                                         and sc.column_id = sep.minor_id
                                         and sep.name = 'MS_Description'
	left join (
		select kcu.TABLE_SCHEMA, kcu.TABLE_NAME, kcu.CONSTRAINT_NAME, kcu.COLUMN_NAME, kcu.ORDINAL_POSITION
	  from INFORMATION_SCHEMA.TABLE_CONSTRAINTS as tc
	  join INFORMATION_SCHEMA.KEY_COLUMN_USAGE as kcu
		on kcu.CONSTRAINT_SCHEMA = tc.CONSTRAINT_SCHEMA
	   and kcu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME
	   and kcu.TABLE_SCHEMA = tc.TABLE_SCHEMA
	   and kcu.TABLE_NAME = tc.TABLE_NAME
	 where tc.CONSTRAINT_TYPE = 'PRIMARY KEY') p 
		on p.TABLE_NAME=st.name and p.COLUMN_NAME=sc.name
    where 
		--st.name in ('systemEntity')
		--st.name in ('stateMachine')
		--st.name in ('projectVersion')
		--st.name in ('systemEntity', 'systems', 'systemTemplate')
		--st.name in ('globalEvent')
		--st.name in ('stateMachineEvent', 'globalEvent')
		--st.name in ('stateMachine','stateMachineState','stateMachineEvent','stateMachineEvent2State')
		--st.name in ('stateMachineEvent')--,'globalEvent','stateMachine')
		st.name in ('stateMachineState')
	order by st.name, sc.column_id
