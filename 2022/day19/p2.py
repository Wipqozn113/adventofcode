# Inject into python path
import sys
import os
import re 
import math
from file_read_backwards import FileReadBackwards

sys.path.insert(0, os.path.abspath(os.path.join(os.path.dirname(__file__), '../..')))

#from utils.graphs import StateGraph, State, Edge

class Cost:
    def __init__(self, ore=0, clay=0, obsidian=0):
        self.ore = ore
        self.clay = clay
        self.obsidian = obsidian

    def costs(self):
        return "({},{},{})".format(self.ore, self.clay, self.obsidian)

    def CanAfford(self, ore=0, clay=0, obsidian=0):
        return ore >= self.ore and clay >= self.clay and obsidian >= self.obsidian

    def UpdateState(self, state):
        state.ore -= self.ore
        state.clay -= self.clay
        state.obsidian -= self.obsidian

    def NeverAfford(self, state, time):
        ore = (state.ore_robots * time) + state.ore
        clay = (state.clay_robots * time) + state.clay
        obsidian = (state.obsidian_robots * time) + state.obsidian

        # will never have enough resources to afford this
        return (self.ore > ore) and (self.clay > clay) and (self.obsidian > obsidian)


class Blueprint:
    max_ore = 0
    max_clay = 0
    max_obsidian = 0

    def __init__(self, num=0, input=None):
        inp = input.split(":")[1].split(".")
        self.num = num
        self.ore = self.OreCost(inp[0])
        self.clay = self.ClayCost(inp[1])
        self.obsidian = self.ObsidianCost(inp[2])
        self.geode = self.GeodeCost(inp[3])
        self.final_state = None

    def CanNeverAffordGeode(self, state, time):
        return self.ore.NeverAfford(state, time)

    def CanNeverAffordObs(self, state, time):
        return self.obsidian.NeverAfford(state, time)

    def CanNeverAffordClay(self, state, time):
        return self.clay.NeverAfford(state, time)

    def CanNeverAffordGeode(self, state, time):
        return self.clay.NeverAfford(state, time)

    def PrintMe(self):
        print("Ore: {} Clay: {} Obsidian: {} Geode: {}".format(self.ore.costs(), self.clay.costs(), self.obsidian.costs(), self.geode.costs()))

    @property
    def QualityLevel(self):
        return self.num * self.final_state.geodes

    #Each ore robot costs 2 ore. 
    def OreCost(self, input):
        ore = int(input.strip().split(" ")[4])
        if ore > self.max_ore:
            self.max_ore = ore
        return Cost(ore=ore)
    

    def CanAffordOre(self, state):
        return self.ore.CanAfford(ore=state.ore)
    
    #Each clay robot costs 4 ore. 
    def ClayCost(self, input):
        ore = int(input.strip().split(" ")[4])
        if ore > self.max_ore:
            self.max_ore = ore
        return Cost(ore=ore)

    def CanAffordClay(self, state):
        return self.clay.CanAfford(ore=state.ore)

    #Each obsidian robot costs 4 ore and 17 clay. 
    def ObsidianCost(self, input):
        ore = int(input.strip().split(" ")[4])
        clay = int(input.strip().split(" ")[7])
        if ore > self.max_ore:
            self.max_ore = ore
        if clay > self.max_clay:
            self.max_clay = clay
        return Cost(ore=ore,clay=clay)

    def CanAffordObsidian(self, state):
        return self.obsidian.CanAfford(ore=state.ore, clay=state.clay)

    #Each geode robot costs 3 ore and 11 obsidian.
    def GeodeCost(self, input):
        ore = int(input.strip().split(" ")[4])
        obsidian = int(input.strip().split(" ")[7])
        if ore > self.max_ore:
            self.max_ore = ore
        if obsidian > self.max_obsidian:
            self.max_obsidian = obsidian
        return Cost(ore=ore, obsidian=obsidian)

    def CanAffordGeode(self, state):
        return self.geode.CanAfford(ore=state.ore, clay=state.clay, obsidian=state.obsidian)

class RobotFactory:
    def __init__(self, blueprint):
        self.blueprint = blueprint

    def DoNothing(self, state, states, depth):
        # Always create  a DoNothing state if no other states were created
        if len(states) == 0:
            return True

        # We can create a geode, so that's all we want to do
        if self.blueprint.CanAffordGeode(state):
            return False

        # No point saving up ore if we haven't built a clay or ore robot yet
        if ((self.blueprint.CanAffordClay(state) and state.clay_robots == 0) and
            (self.blueprint.CanAffordOre(state) and state.ore_robots == 1)):
            return False

        # Same idea. Build an obsidian robot if we can, and haven't built one yet.
        if ((self.blueprint.CanAffordObsidian(state) and state.obsidian_robots == 0) and
            (self.blueprint.CanAffordClay(state) and state.clay_robots >= 1) and
            (self.blueprint.CanAffordOre(state) and state.ore_robots >= 1)):
            return False

        if (state.ore > self.blueprint.max_ore * 3) or (state.clay > self.blueprint.max_clay * 3) or (state.obsidian > self.blueprint.max_obsidian * 3):
            return False

        # We're way too far down to be missing 
        #if depth >= 20 and state.obsidian_robots <= 1 and state.geode_robots == 0:
        #    return False

        # Otherwise, save up
        return True

    def StartRobot(self, state, depth, time_left):        
        states = []

        if self.blueprint.CanAffordGeode(state):
            nstate = state.Copy()
            self.blueprint.geode.UpdateState(nstate)
            nstate.geode_robots_building += 1
            states.append(nstate)

            return states

        if state.BuildGeode(self.blueprint, time_left):
            if not state.build_geode:
                nstate = state.Copy()
                nstate.build_geode = True    
                states.append(nstate)      
        
        if state.BuildObs(self.blueprint, time_left):
            if not state.build_obs:
                nstate = state.Copy()
                nstate.build_obs = True
            if self.blueprint.CanAffordObsidian(state) and self.blueprint.max_obsidian > state.obsidian_robots:
                nstate = state.Copy()
                self.blueprint.obsidian.UpdateState(nstate)
                nstate.obsidian_robots_building += 1
                states.append(nstate)
            elif not state.build_obs:
                states.append(nstate)

        if state.BuildClay(self.blueprint, time_left):
            if not state.build_clay:
                nstate = state.Copy()
                nstate.build_clay = True
            if self.blueprint.CanAffordClay(state) and state.clay_robots < self.blueprint.max_clay:
                nstate = state.Copy()
                self.blueprint.clay.UpdateState(nstate)
                nstate.clay_robots_building += 1
                states.append(nstate)
            elif not state.build_clay:
                states.append(nstate)
        
        if state.BuildOre(self.blueprint, time_left):
            if not state.build_ore:
                nstate = state.Copy()
                nstate.build_ore = True
            if self.blueprint.CanAffordOre(state) and self.blueprint.max_ore > state.ore_robots:
                nstate = state.Copy()
                self.blueprint.ore.UpdateState(nstate)
                nstate.ore_robots_building += 1
                states.append(nstate)
            elif not state.build_ore:
                states.append(nstate)

        # We don't always want to append a "Do nothing" step
        #if self.DoNothing(state, states, depth):
        #    states.append(state.Copy())
        

        #print("===========")
        #for st in states:
        #    st.PrintMe()
        #print("============")

        return states

    def FinishRobot(self, state):
        state.FinishRobots()


class RoboState:
    ore = 0
    clay = 0
    obsidian = 0
    geodes = 0
    ore_robots = 0
    ore_robots_building = 0
    build_ore = False
    clay_robots = 0
    clay_robots_building = 0
    build_clay = False
    obsidian_robots = 0
    obsidian_robots_building = 0
    build_obs = False
    geode_robots = 0
    geode_robots_building = 0
    build_geode = False

    def __init__(self, ore=0, clay=0, obsidian=0, geodes=0, ore_robots=0, clay_robots=0, obsidian_robots=0, geode_robots=0):
        self.ore = ore
        self.clay = clay
        self.obsidian = obsidian
        self.geodes = geodes
        self.ore_robots = ore_robots
        self.clay_robots = clay_robots
        self.obsidian_robots = obsidian_robots
        self.geode_robots = geode_robots

    def StillSaving(self, blueprint):
        # not saving anything
        if not self.Saving():
            return False

        # Still saving up for something we can't afford
        if self.build_geode and not self.blueprint.CanAffordGeode(self):
            return True

        if self.build_obs and not self.blueprint.CanAffordObsidian(self):
            return True

        if self.build_clay and not self.blueprint.CanAffordClay(self):
            return True

        if self.build_ore and not self.blueprint.CanAffordOre(self):
            return True

        # We can afford whatever we're saving for
        return False

    def Saving(self):
        return self.build_geode or self.build_obs or self.build_clay or self.build_ore

    def BuildGeode(self, blueprint, remaining_time=0):
        # Already tryin to build
        if self.build_geode:
            return True

        # We're already saving up for something else
        if self.Saving():
            return False

        # We lack the robots required to actually save up for a geo
        if self.obsidian_robots == 0:
            return False

        # Not enough time to afford one of these anyways
        if blueprint.CanNeverAffordGeode(self, remaining_time):
            return False

        return True

    def BuildObs(self, blueprint, remaining_time=0):
        # Already tryin to build
        if self.build_obs:
            return True

        # We're already saving up for something else
        if self.Saving():
            return False

        # We lack the robots required to actually save up for an obs
        if self.clay_robots == 0:
            return False

        # We don't need anymore Obs bots
        if self.obsidian_robots > blueprint.max_obsidian:
            return False

        # Not enough time to afford one of these anyways
        if blueprint.CanNeverAffordObs(self, remaining_time):
            return False

        return True

    def BuildClay(self, blueprint, remaining_time=0):
        # Already tryin to build
        if self.build_clay:
            return True

        # We're already saving up for something else
        if self.Saving():
            return False

        # We don't need anymore clay bots
        if self.clay_robots > blueprint.max_clay:
            return False

        # Not enough time to afford one of these anyways
        if blueprint.CanNeverAffordClay(self, remaining_time):
            return False

        return True

    def BuildOre(self, blueprint, remaining_time=0):
        # Already tryin to build
        if self.build_ore:
            return True

        # We're already saving up for something else
        if self.Saving():
            return False

        # We don't need anymore clay bots
        if self.ore_robots > blueprint.max_ore:
            return False

        # Not enough time to afford one of these anyways
        if blueprint.CanNeverAffordGeode(self, remaining_time):
            return False

        return True

    def __hash__(self):
        return hash((self.ore.__hash__(), self.clay.__hash__(), self.obsidian.__hash__(), self.geodes.__hash__(), 
            self.ore_robots.__hash__(), self.clay_robots.__hash__(), self.obsidian_robots.__hash__(), self.geode_robots.__hash__(),
            self.ore_robots_building.__hash__(), self.clay_robots_building.__hash__(), self.obsidian_robots_building.__hash__(), self.geode_robots_building.__hash__()))

    def __eq__(self, other):
        return (self.ore == other.ore and self.clay == other.clay and
                self.obsidian == other.obsidian and self.geodes == other.geodes and
                self.ore_robots == other.ore_robots and self.clay_robots == other.clay_robots and
                self.obsidian_robots == other.obsidian_robots and self.geode_robots == other.geode_robots and 
                self.ore_robots_building == other.ore_robots_building and self.clay_robots_building == other.clay_robots_building and
                self.obsidian_robots_building == other.obsidian_robots_building and self.geode_robots_building == other.geode_robots_building)       

    def GoMode(self, blueprint):
        return (self.ore_robots == blueprint.geode.ore and 
            self.clay_robots == blueprint.clay.ore and
            self.obsidian_robots == blueprint.obsidian.ore)

    def EqualRobots(self, other):
        return (self.ore_robots == other.ore_robots and self.clay_robots == other.clay_robots and
            self.obsidian_robots == other.obsidian_robots and self.geode_robots == other.geode_robots)

    def FinishRobots(self):
        self.ore_robots += self.ore_robots_building
        self.ore_robots_building = 0
        self.build_ore = False
        self.clay_robots += self.clay_robots_building
        self.clay_robots_building = 0
        self.build_clay = False
        self.obsidian_robots += self.obsidian_robots_building
        self.obsidian_robots_building = 0
        self.build_obs = False
        self.geode_robots += self.geode_robots_building
        self.geode_robots_building = 0
        self.build_geode = False

    def PrintMe(self):
        print("Ore: {} ({}) Clay: {} ({}) Obsiodian: {} ({}) Geodes: {} ({})".format(
                self.ore, self.ore_robots, self.clay, self.clay_robots, 
                self.obsidian, self.obsidian_robots, self.geodes, self.geode_robots))

    def Copy(self):
        return RoboState(self.ore, self.clay, self.obsidian, self.geodes, 
            self.ore_robots, self.clay_robots, self.obsidian_robots, self.geode_robots)

    def AreEqual(self, other):
        if (self.ore == other.ore and self.clay == other.clay and
                self.obsidian == other.obsidian and self.geodes == other.geodes and
                self.ore_robots == other.ore_robots and self.clay_robots == other.clay_robots and
                self.obsidian_robots == other.obsidian_robots and self.geode_robots == other.geode_robots):
            return True
        
        return False

def CreateBlueprints(filename):
    num = 1
    blueprints = []
    with open(filename) as file:
        for line in file:
            blueprint = Blueprint(num, line)
            blueprints.append(blueprint)
            num += 1

    return blueprints

class Edge:
    def __init__(self, parent, child, state):
        self.parent = parent
        self.child = child
        self.state = state


class Node:
    children = []
    edges = []
    
    def __init__(self, state, robotfactory, depth, max_depth, graph):
        self.state = state
        self.robotfactory = robotfactory
        self.depth = depth
        self.max_depth = max_depth        
        self.graph = graph

    def Crawl(self):
        #if not self.graph.IsHighestGeoDepth(self.state, self.depth, self.state.geodes):
        #     geo, sta = self.graph.best_geo[self.depth]
        #    if self.state.EqualRobots(sta):                
        #        return self.state.geodes            


        # At maximum depth, so stop crawling
        if self.max_depth == self.depth:
            return self.state.geodes

        # We've saving still, so yolo swag town
        if self.state.StillSaving(self.graph.blueprint):
            state = self.state.Copy()
            while state.StillSaving(self.graph.blueprint):
                # At maximum depth, so stop crawling
                if self.max_depth == state.depth:
                    return state.geodes
                state.depth += 1
                state.ore += state.ore_robots
                state.clay += state.clay_robots
                state.obsidian += state.obsidian_robots
                state.geodes += state.geode_robots            

        #print(self.depth)
        max_geodes = self.state.geodes
        states = self.robotfactory.StartRobot(self.state, self.depth, self.graph.max_depth - self.depth)

        # No longer the best, so don't bother going on
        if not self.graph.IsLowestState(self.state, self.depth):
            return max_geodes

        for state in states:
            state.ore += state.ore_robots
            state.clay += state.clay_robots
            state.obsidian += state.obsidian_robots
            state.geodes += state.geode_robots
            self.robotfactory.FinishRobot(state)
            child = self.CreateChild(state)
            if child is not None:                
                geodes = child.Crawl()
                if geodes > max_geodes:
                    max_geodes = geodes

        return max_geodes

    def CreateChild(self, state):
        if self.graph.IsLowestState(state, self.depth + 1, True) and self.graph.CanOutPace(state, self.depth):
            node = Node(state.Copy(), self.robotfactory, self.depth + 1, self.max_depth, self.graph)
            self.children.append(node)
            return node
        
        return None

class Graph:
    def __init__(self, blueprint, max_depth):
        self.blueprint = blueprint
        self.max_depth = max_depth
        self.start_state = RoboState(ore_robots=1)
        self.robot_factory = RobotFactory(blueprint)
        self.state_dict = {}
        self.best_geo = {}
        self.root = Node(self.start_state, self.robot_factory, 0, self.max_depth, self)
        self.geo_tracker = {}

    def IsHighestGeoDepth(self, state, depth, geodes):
        if depth not in self.best_geo:
            self.best_geo[depth] = (geodes, state)

        if self.best_geo[depth][0] <= geodes:
            self.best_geo[depth] = (geodes, state)
            return True

        return False

    def MaxGeodes(self, geo, time, bots=0):
        mx = (time * (time + 1)) / 2

        return geo + (mx - (bots * (bots + 1)) / 2)

    def MinGeodes(self, geo, time, bots=0):
        return geo + (time * bots)

    def CanOutPace(self, state, depth):
        if depth not in self.geo_tracker:
            self.geo_tracker[depth] = (state.geodes, state.geode_robots)
            return True

        md = self.max_depth
        for dpth, geo in self.geo_tracker.items():
            geos = geo[0]
            robos = geo[1]

            if self.MaxGeodes(state.geodes, md - depth, state.geode_robots) > self.MinGeodes(geos, md - dpth, robos):
                return True

            return False

    def IsLowestState(self, state, depth, fail_equality=False):
        # Unseen state. Automatically lowest.
        if state not in self.state_dict:
            self.state_dict[state] = depth
            return True

        # State exists. Check if a better state exists.
        d = self.state_dict[state]

        # Better State exists. Do nothing.
        if depth > d:
            return False

        # Special case. When considering if we should crawl, we consider equality a failure.
        # When we do a doublecheck inside of Crawl(), we don't conider equality a failure.         
        if fail_equality and depth == d:
            return False

        # New lowest state. Update
        if depth < d:
            self.state_dict[state] = depth

        # Either new lowest state or tied for lowest, so go ahead
        return True       
        

    def DFS(self, root=None):
        if root is None:
            root = self.root

        max_geodes = root.Crawl()
        print(max_geodes)
        return max_geodes * self.blueprint.num  
  
def Run(blueprints, max_depth=32):
    ql_sum = 1
    for blueprint in blueprints:
        blueprint.PrintMe()
        graph = Graph(blueprint, max_depth)
        ql_sum *= graph.DFS()
    return ql_sum

blueprints = CreateBlueprints("input.in")
ql_sum = Run(blueprints)
print("=======")
print(ql_sum)
'''
  Each ore robot costs 4 ore.
  Each clay robot costs 2 ore.
  Each obsidian robot costs 3 ore and 14 clay.
  Each geode robot costs 2 ore and 7 obsidian.
'''

