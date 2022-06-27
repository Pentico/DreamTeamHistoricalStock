create table stockprices.stockprices
(
    information varchar not null,
    symbol varchar not null,
    lastrefreshed timestamp with time zone not null,
    outputsize varchar not null,
    timezone varchar not null,
    timeseries varchar not null,
    id uuid not null
        constraint stockprices_pk
            primary key,
    datecreated timestamp with time zone default current_timestamp not null
);

