
Cambiar el usuario a nodejs
sudo su - nodejs

Clonar el repositorio necesario para la clase
git clone https://github.com/edisoncast/linux-platzi
exit

Cambiar el nombre a la carpeta de linux-platzi a server

Se cambia nombre carpeta
mv /linux-platzi /server

Corregir los errores en el archivo de configuración del servicio en /lib/systemd/system/platzi@.service

User=nodejs
WorkingDirectory=/home/nodejs/server
ExecStart=/usr/bin/node /home/nodejs/server/server.js


Iniciar el servicio (debemos estar en la carpeta /server/configuracion_servidor/bash)
./enable.sh
./start.sh

Iniciar el servicio de Nginx (Apagar antes Apache si es necesario)
sudo systemctl start nginx

Una vez en la carpeta /etc/nginx/sites-available/ eliminar el contenido de la configuración de Nginx
sudo truncate -s0 default

Editar el archivo de configuración
sudo vim default

# Una vez en el archivo, escribir lo siguiente

server  {
	listen 80 default_server;
	listen [::]:80 default_server;
	
	server_name _;
	
	location / {
		proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
		proxy_set_header Host $host;
		proxy_http_version 1.1;
		proxy_pass http://backend;
	}
}

upstream backend {
	server 127.0.0.1:3000;
	server 127.0.0.1:3001;
	server 127.0.0.1:3002;
	server 127.0.0.1:3003;
}

Validamos que la configuración establecida fue correcta
sudo nginx -t

Reiniciamos nginx
sudo systemctl restart nginx

Probamos todo haciendo un curl a localhost
curl localhost

