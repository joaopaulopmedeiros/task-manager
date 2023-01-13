CREATE DATABASE task_manager;
USE task_manager;
CREATE TABLE tasks(
	id varchar (255) NOT NULL,
	title varchar (255) NOT NULL,
    status smallint NOT NULL DEFAULT 1,
    author varchar (255) NOT NULL, 
    created_at datetime NOT NULL,
    PRIMARY KEY(id)
);
INSERT INTO tasks (id, title, author, status, created_at) VALUES ('1111e7b9-7555-eeff-b863-df658440820d', 'Tarefa de exemplo','joaopaulopmedeiros@gmail.com', '1', '2023-01-13 23:38:45.000000');
