Para correrlo desde visual code

python server.py

localhost:8080/predict

Retornara

{"prediccion":[7.44343]}


O sea, el server.py que corre tiene un endpoint predict  pero no le pasa datos, los tiene estáticos

Y este llama a un modelo que fue creado que esta en el directorio models

De esta forma se crea y entrena un modelo de machine learning, luego creas una web api que lo consume, y le haces una llamada en el browser

Lo que no tiene es hacer un programa (que podría ser Python o C# que hace la llamada a la web api, y le envia los parámetros en vez de tenerlos estaticos


# profesional_scikitlearn_platzi
Repositorio de código usado durante el Curso Profesional de Scikit-Learn para Platzi.

En este repositorio podrás encontrar 16 ramas relacionadas con el curso, en orden:

1. preparacion_datos_pca
2. implementacion_algoritmo_pca
3. kernel_y_pca
4. implementacion_lasso_ridge 
5. preparacion_regresion_robusta
6. implementacion_regresion_robusta
7. preparacion_datos_bagging
8. implementacion_bagging
9. implementacion_boosting
10. implementacion_kmeans
11. implementacion_meanshift
12. implementacion_crossval
13. implementacion_randomizedSearchCV
14. revision_arquitectura
15. creacion_exportacion_modelo
16. creacion_servidor_flask