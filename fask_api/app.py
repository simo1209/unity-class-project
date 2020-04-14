import json
from flask import request
from flask import Flask
from flask import jsonify
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
    cur.execute(''' SELECT Recipe FROM cocktailrecipes
                    WHERE title = '{}';  '''.format(title))
    recipe = cur.fetchone()
    print(recipe)
    if(not recipe):
        return "Bad request", 404
    return jsonify(recipe)


@app.route('/jokes/<number>' , methods=['GET'])
def get_joke(number):
    cur = mysql.connection.cursor()
    cur.execute(''' SELECT Joke FROM dadjokes LIMIT {},1;  '''.format(number))
    joke = cur.fetchone()
    return jsonify(joke)




if __name__=='__main__':
    app.run(debug=True)
