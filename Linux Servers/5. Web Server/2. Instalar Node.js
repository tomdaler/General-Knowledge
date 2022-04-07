
Clonar el repositorio necesario para realizar la clase
git clone https://github.com/edisoncast/linux-platzi

cd /linux-platzi


Instalar Node.js y npm
sudo apt install nodejs npm


Posicionados en el home, descargar Node 10
cd \
curl -sL https://deb.nodesource.com/setup_10.x -o node_setup.sh


Instalar Node 10
sudo bash node_setup.sh


Instalar gcc, g++ y make
sudo apt install gcc g++ make


Finalizar el proceso de instalación de la versión 10 de Node
sudo apt install -y nodejs
node -v

Agregar el usuario nodejs si todavía no lo creaste
sudo adduser nodejs


En la carpeta de linux-platzi, ejecutar el archivo server.js
cd \linux-platzi
node server.js


Crear un archivo de configuración para el servicio de Node
sudo vim /lib/systemd/system/platzi@.service

# Una vez creado el archivo, llenarlo con la siguiente información

[Unit]
Description=Balanceo de carga para Platzi
Documentation=https://github.com/edisoncast/linux-platzi
After=network.target

[Service]
Enviroment=PORT=%i
Type=simple
User=nodejs
WorkingDirectory=/home/nodejs/linux-platzi
ExecStart=/usr/bin/node /home/nodejs/linux-platzi/server.js
Restart-on=failure

[Install]
WantedBy=multi-user.target