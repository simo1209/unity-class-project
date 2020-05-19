class Room:
    def __init___(self, name, display):
        self.name = name
        self.user1 = None
        self.user2 = None
        self.display = None

    
    def is_room_full(self):
        return self.user1 and self.user2

    def add_user(self, user):
        if(self.is_room_full):
            return -1
    
        if(self.user1 == None):
            self.user1 = user
            return 0

        self.user2 = user
        return 0

    def add_display(self, display):
        if(self.display):
            self.display = display
            return 0

        return -1

    def get_user_id_from_sid(self, sid):
        if(self.user1 == sid):
            return 1
        
        if(self.user2 == sid):
            return 2

        return -1

rooms = []

def is_in_rooms(room_name):
    for current_room in rooms:
        if(current_room.name == room_name):
            return True

    return False

def find_room_by_name(room_name):
    for current_room in rooms:
        if(current_room.name == room_name):
            return current_room