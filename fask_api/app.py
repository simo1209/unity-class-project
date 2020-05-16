import json
import os.path
from flask import request
from flask import Flask
from flask import jsonify
from flask import send_file, send_from_directory, safe_join, abort
from flask_mysqldb import MySQL

app = Flask(__name__)

app.config['MYSQL_HOST'] = 'localhost'
app.config['MYSQL_USER'] = 'BrewmasterApi'
app.config['MYSQL_PASSWORD'] = 'wearenotprepared123'
app.config['MYSQL_DB'] = 'brewmastersdata'
app.config['MYSQL_CURSORCLASS'] = 'DictCursor'

mysql = MySQL(app)

@app.route('/titles' , methods=['GET'])
def get_titles():
    cur = mysql.connection.cursor()
    cur.execute(''' SELECT title FROM cocktailrecipes;  ''')
    titles = cur.fetchall()
    print(titles)
    return jsonify(titles)

@app.route('/recipes/<title>' , methods=['GET'])
def get_recipe(title):
    cur = mysql.connection.cursor()
    cur.execute(''' SELECT Recipe, Title FROM cocktailrecipes
                    WHERE title = '{}';  '''.format(title))
    recipe = cur.fetchone()
    print(recipe)
    if(not recipe):
        return "Bad request", 404
    return jsonify(recipe)


@app.route('/randjoke' , methods=['GET'])
def get_joke():
    cur = mysql.connection.cursor()
    cur.execute(''' SELECT * from dadjokes order by rand() limit 1;  ''')
    joke = cur.fetchone()  
    return jsonify(joke)


@app.route("/game_images/<image_name>")
def get_image(image_name):
    try:
        return send_from_directory(directory='..\\images\\', filename=image_name, as_attachment=False)
    except FileNotFoundError:
        abort(404)


if __name__=='__main__':
    app.run(debug=True)
