# Inject into python path
import sys
import os
import re 
import math
import itertools

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../..')))

from utils.graphs import Graph, Node

class Valve:
    def __init__(self, name, flow_rate):
        self.name = name
        self.flow_rate = flow_rate
        self.open = False
    
    @property
    def Key(self):
        return self.name

    def Reset(self):
        self.open = False

    def Action(self, pressure, time):
        if self.flow_rate > 0 and time > 0:    
            self.open = True
            #time -= 1
            pressure += self.flow_rate * time


        return pressure, time

    @property
    def Closed(self):
        return not self.open

class CacheInfo:
    def __init__(self, path, pressure, time):
        self.path = path[:]
        self.pressure = pressure
        self.time = time

class PathCache:
    def __init__(self):
        self.cache = {}
        self.path_length = None

    def GetKey(self, path):
        key = ""
        for node in path:
            key += "{}:".format(node.Key)
        return key

    def SetCache(self, path, pressure, time):
        key = self.GetKey(path)
        if key not in self.cache:
            self.cache[key] = CacheInfo(path, pressure, time)

    def GetBestCache(self, path):
        if self.path_length is None:
            self.path_length = len(path)
        n = self.path_length
        while n > 0:
            key = self.GetKey(path[:n])
            if key in self.cache:
                return self.cache[key]
            n -= 1

        return None

class Valves:
    def __init__(self, valves, graph):
        self.all_valves = valves
        self.nonzero_valves = []
        for key, valve in valves.items():
            if valve.obj.flow_rate > 0:
                self.nonzero_valves.append(valve)
        self.nonzero_valves.sort(key=lambda x: x.obj.flow_rate, reverse=True)
        self.valve_graph = graph
        self.cache = PathCache()

    def FindBestPathFaster(self):
        valves = self.nonzero_valves
        c = len(valves)
        n = 2
        root = self.valve_graph.root
        highest_pressure = 0
        while n < c:
            start = valves[:c-n]
            pressure, time = self.CalcPressure(root, 30, 0, 0, start)
            end = valves[c-n:]
            ends = itertools.permutations(end)
            st = valves[:c-n][-1]
            for e in ends:
                p,t = self.CalcPressure(st, time, c - n, pressure, end)
                if pressure + p > highest_pressure:
                    highest_pressure = pressure + p
            n += 1

        return highest_pressure

    def CalcPressure(self, start, time_limit, n, pressure, path):
        while n < len(path):               
            target = path[n]    
            dist = start.distance[target.Key]
            time_limit -= (dist + 1)    
            if time_limit <= 0:
                break
            pressure += time_limit * target.obj.flow_rate
            #self.cache.SetCache(path[:n+1], pressure, time_limit)

            start = target
            n += 1

        return pressure, time_limit

    def FindAverage(self, valves):
        f = 0
        for valve in valves:
            f += valve.obj.flow_rate
        avg = math.floor(f / len(valves))

        return avg
        
    def FindClosestAboveAvg(self, start, valves):
        avg = self.FindAverage(valves)
        best = None
        for valve in valves:
            if valve.obj.flow_rate >= avg:
                if best is None or start.distance[valve.Key] < start.distance[best.Key]:
                    best = valve
                elif (start.distance[valve.Key] == start.distance[best.Key]) and (valve.obj.flow_rate > best.obj.flow_rate):
                    best = Valve
        return best

    def FindBestPath2(self):
        root = self.valve_graph.root
        avg = self.FindAverage(self.nonzero_valves)
        time = 30
        node = root
        valves = self.nonzero_valves[:]
        path = []
        while len(valves) > 0:
            next = self.FindClosestAboveAvg(node, valves)
            path.append(next)
            valves.remove(next)
            node = next

        pressure, time = self.CalcPressure(node, time, 0, 0, path)

        return pressure
        
            
                


    def FindBestPath(self):
        #paths = itertools.permutations(self.nonzero_valves)
       # paths = []
       # for valve in self.nonzero_valves:
       #     paths.append([valve])
        highest_pressure = 0
        count = 0
        f = 0
        for valve in self.nonzero_valves:
            f += valve.obj.flow_rate
        avg = math.floor(f / len(self.nonzero_valves))
        val_abo_avg = []
        val_bel_avg = []
        for valve in self.nonzero_valves:
            if valve.obj.flow_rate >= 16:
                val_abo_avg.append(valve)
            else:
                val_bel_avg.append(valve)

        paths = itertools.permutations(val_abo_avg)
        best_path = []
        for pt in paths:
            count += 1
            path = list(pt)
            time_limit = 30
            pressure = 0
            start = self.valve_graph.root
            n = 0
            pressure, time = self.CalcPressure(start, time_limit, n, pressure, path)
            #print("On path ", count, "Pressure: ", pressure)
            if pressure > highest_pressure:
                highest_pressure = pressure
                front_path = path       

        highest_pressure = 0
        back_paths = itertools.permutations(val_bel_avg)
        for pt in back_paths:
            count += 1
            pth = list(pt)
            path = front_path + pth
            time_limit = 30
            pressure = 0
            start = self.valve_graph.root
            n = 0
            pressure, time = self.CalcPressure(start, time_limit, n, pressure, path)
            #print("On path ", count, "Pressure: ", pressure)
            if pressure > highest_pressure:
                highest_pressure = pressure

        return highest_pressure
        

def CreateValves(filename):
    valves = {}
    with open(filename) as file:
        for line in file:
            ln = line.split(";")[0]
            ln = ln.split(" ")
            name = ln[1]
            flow_rate=ln[-1].split("=")[1]
            valve = Valve(name, int(flow_rate))
            node = Node(valve)
            valves[name] = node
    
    return valves

def CreateGraph(filename, valves, root="AA"):
    graph = Graph(valves[root])
    with open(filename) as file:
        for line in file:
            line = line.split(";")
            name = line[0].split(" ")[1]
            splitter = "valves" if "valves" in line[1] else "valve"
            tunnels = line[1].split(splitter)[1].split(",")
            for tunnel in tunnels:
                tun = tunnel.strip()
                valves[name].AddChild(valves[tun])
            graph.AddNode(valves[name])

    graph.FindAllShortestPaths()
    return graph

def PrintValves(valves):
    for key, valve in valves.items():
        name = valve.obj.name
        flow_rate = valve.obj.flow_rate
        children = ""
        for child in valve.children:
            children += child.obj.name + ",";
        print("Valve {} has flow rate={}; tunnels lead to valves {}".format(name, flow_rate, children))

def FuckingAround(valves, valve_cave):
    root = valve_cave.valve_graph.root
    for valve in valve_cave.nonzero_valves:
        dist = root.distance[valve.Key]
        output = "{} {} {} | ".format(valve.obj.name,valve.obj.flow_rate, dist)
        output += "{}".format(dist * valve.obj.flow_rate)
        print(output)



filename = "test.in"
valves = CreateValves(filename)
graph = CreateGraph(filename, valves)      
valve_cave = Valves(valves, graph)
#FuckingAround(valves, valve_cave)
print(valve_cave.FindBestPath2())
#print(valve_cave.FindBestPath())
#PrintValves(valves)

