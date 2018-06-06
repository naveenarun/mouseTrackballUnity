clear;
tic
counter = 0;
prevTime = 0;

udpOn = true;

if udpOn
    u = udp('127.0.0.1','RemotePort', 9050, 'LocalPort', 9051);
    u.OutputBufferSize = 1024;
    fopen(u);
end

% initialize
m = MotionSensor('COM3');
m.StartReading;

%%
m.StartReading;

currX = zeros(3000, 1); 
currY = zeros(3000, 1);

nn = 1;
xtot = 0;
ytot = 0;
while nn<1000
    if mod(nn, 3000)== 0
        nn =1;
    end
    nn = nn + 1;
    pause(0.02)
    [x, y] = m.ReadSensor;
    xtot = xtot + x/500;
    ytot = ytot + y/500;
    mystr = ['SetProperty RedSphere localPosition ', num2str(xtot) ,' 2.28 ', num2str(ytot)]
    if udpOn
        fprintf(u, mystr);
    end
end

if udpOn
	fclose(u);delete(u);clear u
end

m.StopReading