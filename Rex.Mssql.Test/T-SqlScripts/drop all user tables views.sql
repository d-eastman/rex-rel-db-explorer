USE RexUnitTesting
GO

--Delete all user tables and views

select '[' + s.name + '].[' + o.name + ']' as twoLevelName, o.type
into #tvList
from sys.objects o inner join sys.schemas s on o.schema_id=s.schema_id
where o.type in ('U','V')

declare @type char(1)
declare @name varchar(200)

declare tvCursor CURSOR for select type, twoLevelName from #tvList order by type desc
OPEN tvCursor

FETCH NEXT FROM tvCursor INTO @type, @name

WHILE @@FETCH_STATUS = 0
BEGIN
	IF @type='V' exec('drop view ' + @name);
	ELSE exec('drop table ' + @name);
	FETCH NEXT FROM tvCursor INTO @type, @name
END
close tvCursor
deallocate tvCursor

drop table #tvList

