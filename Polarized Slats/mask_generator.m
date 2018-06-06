rows = 1080;
cols = 1920;
[xMat,yMat] = meshgrid(0:cols-1,1:rows-1);
outMat = mod(yMat,6) <= 2;
imwrite(outMat,'C:\Users\tsaolab\Desktop\Polarized Slats\even_lowres.bmp')