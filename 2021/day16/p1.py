class Packet:
    def __init__(self, hex=None, binary=None):
        if hex is not None:
            binary = self.DecodeHex(hex)

        self.bits = binary  
        self.version = int(binary[:3], 2)   
        self.type = int(binary[3:6], 2)   
        self.subpackets = []

    def PopulateSubpackets(self):
        type, length, packets = self.Packets()
        while True:
            v = int(packets[:3], 2)
            t = int(packets[3:6], 2)

            if t == 4:
                n = 6
                val = ""
                while True:
                    val += packets[n+1:n+5]
                    n += 5
                    if packets[n-5] == "0":
                        break
 
                self.subpackets.append(Packet(binary=packets[:n]))   
                packets = packets[n:]
            else:
                l = len(packets)
                packet = Packet(binary=packets)
                packets = packet.PopulateSubpackets()
                self.subpackets.append(packet)
                n = len(packets) - l

            if "1" not in packets:
                break

            if type == 0:     
                length -= n
                if length <= 0:
                    break

            elif type == 1:
                if len(self.subpackets) == length:
                    break
            
        return packets

    def Packets(self):
        if self.bits[6] == "0":
            length = int(self.bits[7:22], 2)
            packets = self.bits[22:]
        elif self.bits[6] == "1":
            length = int(self.bits[7:18], 2)
            packets = self.bits[18:]
        return int(self.bits[6]), length, packets


    @property
    def IsLiteral(self):
        return self.type == 4

    @property 
    def IsOperator(self):
        return self.type != 4

    def DecodeHex(self, hex):
        decoded = ""
        for h in hex:
            decoded += bin(int(h, 16))[2:].zfill(4)
        return decoded

    def VersionSum(self):
        sum = self.version
        for packet in self.subpackets:
            sum += packet.VersionSum()
        return sum

with open('p2.in') as file:
    for line in file:
        l = line.split("|")
        # Test case
        if(len(l) == 2):
            packet = Packet(hex=l[0])
            packet.PopulateSubpackets()
            print(packet.VersionSum(), l[1])
        else:
            packet = Packet(hex=l[0])
            packet.PopulateSubpackets()
            print(packet.VersionSum())
