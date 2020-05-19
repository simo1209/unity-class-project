import json
import os.path
import sockets
from flask import request
from flask import Flask
from flask import jsonify
from flask import send_file, send_from_directory, safe_join, abort
from flask_socketio import SocketIO
from flask_socketio import join_room, leave_room
from flask_mysqldb import MySQL
from sockets import *

app = Flask(__name__)

app.config['SECRET_KEY'] = 'secureOver9000'
app.config['MYSQL_HOST'] = 'localhost'
app.config['MYSQL_USER'] = 'BrewmasterApi'
app.config['MYSQL_PASSWORD'] = 'wearenotprepared123'
app.config['MYSQL_DB'] = 'brewmastersdata'
app.config['MYSQL_CURSORCLASS'] = 'DictCursor'
app.config['IMAGES'] = 'images\\'

mysql = MySQL(app)
socketio = SocketIO(app)

@app.route('/recipes' , methods=['GET'])
def get_recipe():
    page_size = request.args.get("size", default = 10, type = int)
    page_number = request.args.get("page", type = int)
    cur = mysql.connection.cursor()

    cur.execute(''' SELECT * FROM cocktailrecipes
                    WHERE RecipeId > {}
                    LIMIT {};  '''
                                    .format(
                                            page_size *(page_number - 1), 
                                            page_size))
                                    

    print(page_size *(page_number - 1))
    print(page_size * (page_number - 1) + page_size)
    recipe = cur.fetchall()
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
        return send_from_directory(app.config['IMAGES'], filename=image_name, as_attachment=False)
    except FileNotFoundError:
        abort(404)


@app.route("/pong/create_room/<room_name>")
def create_room(room_name):
    # send shit here
    if(is_in_rooms(room_name)):
        return 'Room already exists'
    rooms.append(Room(room_name))

@socketio.on('connect_user', namespace = '/pong')
def connect_user(data):
    room_name = data['room']
    room = find_room_by_name(room_name)
    if(room.add_user(request.sid) == -1):
        send('Unable to join room')
        return -1

    print("{} has joined {} as user".format(request.sid,room_name))


@socketio.on('connect_display', namespace = '/pong')
def connect_display(data):
    room = find_room_by_name(data['room'])
    if(room.add_display(request.sid) == -1):
        send('Unable to join room')
        return -1

    print("{} has joined {} as display".format(request.sid, room_name))

@socketio.on('movement', namespace = '/pong')
def movement(data):
    current_room = find_room_by_name(data['room'])
    user_id = current_room.get_user_id_from_sid(request.sid)

    if(user_id == -1):
         send('Permision denied')
         return -1

    message = {"user_id": user_id, "action": data['action']}
    emit('user_action', message, room = current_room.display, namespace = '\pong')
    print("{} made a move".format(request.sid))


if __name__=='__main__':
    socketio.run(app, debug = True)
