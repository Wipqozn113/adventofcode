from functools import cmp_to_key

class Packet:
    def __init__(self, packet, divider=False):
        self.packet = packet
        self.divider = divider

    def Compare(self, other):
        corr_order = self.__compare(self.packet, other.packet)        
        if corr_order is None:
            return 0
        elif corr_order:
            return -1
        else:
            return 1

    def __compare(self, packet1, packet2):
        # handle convert
        if isinstance(packet1, int):
            p1 = [packet1]
        else:
            p1 = packet1

        if isinstance(packet2, int):
            p2 = [packet2]
        else:
            p2 = packet2

        # Cache p2 length
        p2len = len(p2)
        for i in range(len(p1)):
            # p1 is longer than p2, so wrong order
            if i == p2len:
                return False

            # One value is a list, so we need to recurse
            if isinstance(p1[i], list) or isinstance(p2[i], list):
                corr_order = self.__compare(p1[i], p2[i])
                if corr_order is None:
                    continue
                else:
                    return corr_order
            # Both values are integers, so we can actually compare
            else:
                # Correct Order
                if p1[i] < p2[i]:
                    return True
                # Wrong Order
                elif p1[i] > p2[i]:
                    return False
        
        if(len(p1) == p2len):
            return None
        else:
            return len(p1) < p2len

class Pair:
    def __init__(self, packet1, packet2, index):
        self.packet1 = packet1
        self.packet2 = packet2
        self.index = index

    def ComparePairs(self):
        corr_order = self.__compare(self.packet1, self.packet2)

        if corr_order:
            return self.index
        else:
            return 0

    def __compare(self, packet1, packet2):
        # handle convert
        if isinstance(packet1, int):
            p1 = [packet1]
        else:
            p1 = packet1

        if isinstance(packet2, int):
            p2 = [packet2]
        else:
            p2 = packet2

        # Cache p2 length
        p2len = len(p2)
        for i in range(len(p1)):
            # p1 is longer than p2, so wrong order
            if i == p2len:
                return False

            # One value is a list, so we need to recurse
            if isinstance(p1[i], list) or isinstance(p2[i], list):
                corr_order = self.__compare(p1[i], p2[i])
                if corr_order is None:
                    continue
                else:
                    return corr_order
            # Both values are integers, so we can actually compare
            else:
                # Correct Order
                if p1[i] < p2[i]:
                    return True
                # Wrong Order
                elif p1[i] > p2[i]:
                    return False
        
        if(len(p1) == p2len):
            return None
        else:
            return len(p1) < p2len




def CreatePackets(filename):
    packets = []
    with open(filename) as file:
        for line in file:
            line = line.strip()
            if len(line) > 0:
                packets.append(Packet(eval(line)))
    
    packets.append(Packet([[2]], True))    
    packets.append(Packet([[6]], True))
    
    return packets
            
def PacketCompare(p1, p2):
    return p1.Compare(p2)

packet_key = cmp_to_key(PacketCompare)
packets = CreatePackets("input.in")
packets.sort(key=packet_key)
decoder = 1
i = 0
for packet in packets:
    i += 1
    print(packet.packet)
    if packet.divider:
        decoder *= i
print("Decoder: ", decoder)