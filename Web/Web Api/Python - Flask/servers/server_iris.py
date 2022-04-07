import joblib
import numpy as np

from flask import Flask
from flask import jsonify

# 2 guines _ _ name _ _
app = Flask(__name__)

#POSTMAN PARA PRUEBAS
@app.route('/predict', methods=['GET'])
def predict():
        parser = reqparse.RequestParser()
        parser.add_argument('petal_length')
        parser.add_argument('petal_width')
        parser.add_argument('sepal_length')
        parser.add_argument('sepal_width')

        args = parser.parse_args()  # creates dict

        X_new = np.fromiter(args.values(), dtype=float)  
        # convert input to array

        resultado = {'Prediction': IRIS_MODEL.predict([X_new])[0]}

        return resultado, 200

if __name__ == "__main__":
    model = joblib.load('./models/iris.mdl')
    app.run(port=8080)

