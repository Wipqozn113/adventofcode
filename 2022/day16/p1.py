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

class Valves:
    def __init__(self, valves, graph):
        self.all_valves = valves
        self.nonzero_valves = []
        for key, valve in valves.items():
            if valve.obj.flow_rate > 0:
                self.nonzero_valves.append(valve)
        self.valve_graph = graph

    def FindBestPath(self):
        paths = itertools.permutations(self.nonzero_valves)
        start = self.valve_graph.root
        highest_pressure = 0
        count = 0
        for pt in paths:
            if count % 1000 == 0:
                print("On path ", count, "Highest Pressure: ", highest_pressure)
            count += 1
            path = list(pt)
            time_limit = 30
            pressure = 0
            while len(path) > 0:
                target = path.pop(0)
                self.valve_graph.ResetNodes()
                node = self.valve_graph.BFS(target, start)
                dist = 0
                n = node
                while n.parent is not None:
                    n = n.parent
                    dist += 1
                time_limit -= (dist + 1)    
                pressure += time_limit * node.obj.flow_rate
                start = target

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

    return graph

def PrintValves(valves):
    for key, valve in valves.items():
        name = valve.obj.name
        flow_rate = valve.obj.flow_rate
        children = ""
        for child in valve.children:
            children += child.obj.name + ",";
        print("Valve {} has flow rate={}; tunnels lead to valves {}".format(name, flow_rate, children))

filename = "input.in"
valves = CreateValves(filename)
graph = CreateGraph(filename, valves)      
valve_cave = Valves(valves, graph)
print(valve_cave.FindBestPath())
#PrintValves(valves)

