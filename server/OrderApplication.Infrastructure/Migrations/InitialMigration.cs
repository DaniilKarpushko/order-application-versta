using FluentMigrator;
using FluentMigrator.Expressions;
using FluentMigrator.Infrastructure;

namespace OrderApplication.Infrastructure.Migrations;

[Migration(version: 1, description: "Initial migration")]
public class InitialMigration : IMigration
{
    public void GetUpExpressions(IMigrationContext context)
    {
        context.Expressions.Add(new ExecuteSqlStatementExpression
        {
            SqlStatement = """
                           create table orders (
                           id uuid primary key,
                           created_at timestamptz not null,
                           pickup_date timestamptz not null,
                           weight double precision not null,
                           sender_city varchar(200) not null,
                           sender_address varchar(200) not null,
                           receiver_city varchar(200) not null,
                           receiver_address varchar(200) not null,
                           is_delivered boolean not null);
                           
                           insert into orders (
                               id,
                               created_at,
                               pickup_date,
                               weight,
                               sender_city,
                               sender_address,
                               receiver_city,
                               receiver_address,
                               is_delivered
                           )
                           values
                           (
                               '11111111-1111-1111-1111-111111111111',
                               now(),
                               now() + interval '2 days',
                               12.5,
                               'Москва',
                               'ул. Ленина, 1',
                               'Санкт-Петербург',
                               'Невский проспект, 100',
                               false
                           ),
                           (
                               '22222222-2222-2222-2222-222222222222',
                               now(),
                               now() + interval '1 day',
                               8.3,
                               'Екатеринбург',
                               'пр. Ленина, 50',
                               'Новосибирск',
                               'ул. Красный проспект, 10',
                               false
                           );
                           """,
        });
    }

    public void GetDownExpressions(IMigrationContext context)
    {
        context.Expressions.Add(new ExecuteSqlStatementExpression
        {
            SqlStatement = """
                           drop table if exists orders;
                           """,
        });
    }

    public string ConnectionString => throw new NotSupportedException();
}