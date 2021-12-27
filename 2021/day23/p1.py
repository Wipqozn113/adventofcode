class State:
    def __init__(self, amphipods, cost):
        self.id = ""
        self.cost = cost
        self.amphipods = []
        amphipods.sort()
        for amp in amphipods:
            self.id += amp.LocationId
            self.amphipods.append(amp.CreateCopy())

    def IsValid(self):
        for amp in self.amphipods:
            if amp.IsStuck():
                return False
        
        return True

    def __eq__(self, other):
        self.id == other.id

    def __hash__(self):
        return hash(self.id)


class Node:
    def __init__(self, id, pos, destination=""):
        self.left = None
        self.right = None
        self.down = None
        self.up = None
        self.amphipod = None
        self.destination = destination
        self.pos = pos
        self.id = id

    @property
    def IsRoom(self):
        return self.destination != ""

    @property
    def IsHallway(self):
        return self.destination == ""

    @property
    def IsDoorway(self):
        return self.up is None and self.down is Node

    @property
    def IsOccupied(self):
        return self.amphipod is Amphipod

    @property
    def IsOccupiedByProper(self):
        return self.amphipod is Amphipod and self.amphipod.destination == self.destination

class Amphipod:
    def __init__(self, id, destination, energy_to_move, node=None):
        self.destination = destination
        self.destnodes = []
        self.etm = energy_to_move
        self.node = node
        self.id = destination + id
    
    def CreateCopy(self):
        return Amphipod(self.id, self.destination, self.etm, self.node)

    def IsStuck(self):
        if self.InFinalRoom():
            return False

        for dest in self.destnodes:
            exists, blockers = self.FindPath(self.dest)
            if exists:
                return False

            for blocker in blockers:
            
        
        return True


    @property
    def LocationId(self):
        return f"{self.id}:{self.node.id}"

    @property
    def dest(self):
        return self.destination

    @property 
    def InFinalRoom(self):
        return (((self.destination == self.node.destination) and self.node.down is None)
            or (self.destination == self.node.destination) and self.node.down and self.node.down.amphipod and self.node.down.amphipod.destination == self.node.down.destination)

    # Find Ampipods blocking path
    def PathBlockers(self, current, target):
        blocker = None
        if current.IsOccupied:
            blocker = current.amphipod
        
        if current == target:
            return [blocker] if blocker is Amphipod else []

        if current.up and current.pos != target.pos:
            blockers = self.PathBlocker(current.up, target)
        
        if current.down and current.pos == target.pos:
            blockers = self.PathBlocker(current.down, target)

        if current.pos > target.pos and current.left:
            blockers = self.PathBlocker(current.left, target)
    
        if current.pos < target.pos and current.right:
            blockers = self.PathBlocker(current.right, target)
        
        return blockers.append(blocker) if blocker is Amphipod else blockers   

    # Determine if path exists, and return cost of path
    def FindPath(self, current, target):
        if current.IsOccupied:
            return False, 0
        
        if current == target:
            return True, 0

        if current.up and current.pos != target.pos:
            exists, cost = self.FindPath(current.up, target)
            cost += self.energy_to_move

            return exists, cost
        
        if current.down and current.pos == target.pos:
            exists, cost = self.FindPath(current.down, target)
            cost += self.energy_to_move

            return exists, cost

        if current.pos > target.pos and current.left:
            exists, cost = self.FindPath(current.left, target)
            cost += self.energy_to_move

            return exists, cost
    
        if current.pos < target.pos and current.right:
            exists, cost = self.FindPath(current.right, target)
            cost += self.energy_to_move

            return exists, cost
        
        return False, 0

def Createrooms():
    rooma1 = Node("A")
    rooma2 = Node("A")
    rooma2.up = rooma1
    rooma1.down = rooma2

    roomb1 = Node("B")
    roomb2 = Node("B")
    roomb2.up = roomb1
    roomb1.down = roomb2
    
    roomc1 = Node("C")
    roomc2 = Node("C")
    roomc2.up = roomb1
    roomc1.down = roomb2

    roomd1 = Node("D")
    roomd2 = Node("D")
    roomd2.up = roomb1
    roomd1.down = roomb2

    return [rooma1, roomb1, roomc1, roomd1, rooma2, roomb2, roomc2, roomd2]

def TrimAmphipods(amphipods):
    amps = []
    for amp in amphipods:
        if not amp.InFinalRoom:
            amps.append(amp)

    return amps

class Rooms:
    def __init__(self, rooms):
        self.rooms = {}
        for room in rooms:
            if room.destination not in self.rooms:
                self.rooms[room.destination] = {}
            key = "top" if room.down else "bottom"
            self.rooms[room.destination][key] = room

    def TopDest(self, dest):
        return self.rooms[dest]["top"]

    def BottomDest(self, dest):
        return self.rooms[dest]["bottom"]

    def TargetRoom(self, dest):
        return rooms[dest]["top"] if self.rooms[dest]["bottom"].IsOccupiedByProper() else self.rooms[dest]["bottom"]


amphipods = []
nodes = []
rooms = Createrooms()
states = []

# Populate map
with open('p1.in') as file:
    for line in file:
        i = 0
        n = 0
        for char in line:
            if char in ["A", "B", "C", "D"]:
                amp = Amphipod(char)
                amphipods.append(amp)
                rooms[i].amphipod = amp
                amp.node = rooms[i]
                i += 1
            # Create Tree
            elif char == ".":
                nodes.append(Node())
                if i > 0:                    
                    nodes[i].left = nodes[i-1]
                    nodes[i-1].right = nodes[i]
                    
                    if i in (2, 4, 6, 8):                        
                        # 0, 1, 2, 3
                        nodes[i].down = rooms[n]
                        rooms[n].up = nodes[i]
                        n += 1
                i += 1

class Tunnels:
    def __init__(self, amphipods, rooms):
        self.amphipods = amphipods
        self.rooms = rooms
        self.states = {}

    def FindLowestCost(self, state):
        if state in states:
            if states[state].cost > state.cost:
                states[state] = state
        

        amphipods = TrimAmphipods(amphipods)
        if len(amphipods) == 0:
            break

        moved = False
        for amphipod in amphipods:
            target = rooms.TargetRoom(amphipod.dest)
            pathexists, cost = amphipod.FindPath(amphipod.node, target)
            if pathexists:
                amphipod.node = target
                total_cost += cost
        
        # Amphipods moved, so restart loop
        if moved:
            continue

        for amphipod in amphipods:
            target = rooms.TargetRoom(amphipod.dest)
            blockers = amphipod.PathBlockers(amphipod.node, target)
            

rooms = Rooms()
total_cost = 0
while True:
    amphipods = TrimAmphipods(amphipods)
    if len(amphipods) == 0:
       break

    moved = False
    for amphipod in amphipods:
        target = rooms.TargetRoom(amphipod.dest)
        pathexists, cost = amphipod.FindPath(amphipod.node, target)
        if pathexists:
            amphipod.node = target
            total_cost += cost
    
    # Amphipods moved, so restart loop
    if moved:
        continue

    for amphipod in amphipods:
        target = rooms.TargetRoom(amphipod.dest)
        blockers = amphipod.PathBlockers(amphipod.node, target)
        


#############
#...........#
###A#C#B#D###
  #B#A#D#C#
  #########