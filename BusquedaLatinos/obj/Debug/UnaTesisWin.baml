   M S B A M L   `   `   `  ����F  BMantesisApi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullT9clr-namespace:MantesisApi.Converters;assembly=MantesisApiMantesisApi.Converters  5       BusquedaLatinos!   BusquedaLatinos.UnaTesisWin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      O	converter9clr-namespace:MantesisApi.Converters;assembly=MantesisApi     5      p lTelerik.Windows.Controls.RibbonView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7m iTelerik.Windows.Controls.Docking, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e
 aTelerik.Windows.Controls, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2014.2.729.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
   5      -   5        $
��1200��5      $
��1000��5      $��Detalle de la tesis��5	      )  #Telerik.Windows.Controls.WindowBase    WindowState$  	Maximized=�5       � 	Resources 5      +   %MantesisApi.Converters.EpocaConverter   	epocaConv-   'MantesisApi.Converters.MateriaConverter  materiaConv/   )MantesisApi.Converters.InstanciaConverter  
instanConv,   &MantesisApi.Converters.FuenteConverter  
fuenteConv��%t   &	        &	      &	 &     &	 9       5   
   6#     5   
   6%     5   
   6'     5   
   6$   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��0 �6   .+�5   
   ,  &Telerik.Windows.Controls.RadRibbonView  -   5          Name$ 
MainRibbon��5      $��0��5      "   ApplicationButtonVisibility$ 	Collapsed=�5         ApplicationName$  ��5         WindowIconVisibility$ Visible=�5      .@�5       +  %Telerik.Windows.Controls.RadRibbonTab  .@�5!      -  'Telerik.Windows.Controls.RadRibbonGroup  -   5        $��Navega��5!   )   $��Navegar��67   .@�5"      ��  �� Orientation$ 
Horizontal=�6!   .+�5#      .	  (Telerik.Windows.Controls.RadRibbonButton	  -   5        $��
RbtnInicio��5$      $��Inicio��5&       	  
LargeImage$ Resources/leftdArrow.png��5'      	 	  Size$	 Large=�5(      	  5)      -   5        $��RbtnPrevious��5*      $��Anterior��5,      $ Resources/leftsArrow.png��5-      $	 Large=�5.      	  5/      -   5        $��RbtnNext��50      $��	Siguiente��52      $ Resources/rightsArrow.png��53      $	 Large=�54      	  55      -   5        $��RbtnFin��56      $��Final��58      $ Resources/rightdArrow.png��59      $	 Large=�5:      �� 5;      $ Vertical=�6%   .+�5<      �� $��      Registro:   ��5=   !   $��14(�5>   !   $
��Bold"�5?   !   �� 5@      -   5        
 ��Name$
 LblContador��5A   !   $��      Registro:   ��5B   !   $��14(�5C   !   $
��Bold"�5D   !   5E      5F      5H        5I      -	   5        $��DocsRel��5I   )   $��
Relaciones��68   .@�5J      	  -
   5         	 Name$ BtnDelRelacion��5K      $��68��5L      $��Bottom=�5M      $! Resources/flatcross_128.png��5O      $	 Large=�5P      .��5Q      �� $ Vertical=�6%   .+�5R      �� $��Center=�6(   $��Eliminar��6E   �� 5S      $��Center=�6(   $��	relación��6E   5T      5U      5V        5W      $��Exportar��6)   .@�5X      �� $ 
Horizontal=�6!   .+�5Y      	  -   5        $��RbtnClipboard��5Z      $��Portapapeles��5\      $ Resources/clipboard.png��5]      $	 Large=�5^      	  5`      -   5        $��RBtnWord��5a      $
��Word��5c      $ Resources/word_256.png��5d      $	 Large=�5e       	  
SmallImage$ Resources/word_128.png��5f      5g      5h      5i      5j      � 5l   
   $��1��6    � ColumnDefinitions 5m      �� 5n      $
��.25* �6#   $	��300��60   �� 5o      $��* �6#   .+�5r      � $��0��6   .+�5s      �� $��Stretch=�6   $��Stretch=�6;   .@�5t      �� $��Datos Generales��6   .��5u      � ���	#FFE5E5E5.+�5v      �� $��	10,10,0,0q�5w   !   $
��Left=�5x   !   $	��Top=�5y   !   $��Reg. Digital��5z   !   �� 5{      $��
10,110,0,0q�5|   !   $
��Left=�5}   !   $	��Top=�5~   !   $��Época:��5   !   �� 5�      $��	11,41,0,0q�5�   !   $
��Left=�5�   !   $	��Top=�5�   !   $)��#Clave o número de identificación:��5�   !   �� 5�      $	��269��5�   !   $��82��5�   !   $��
11,240,0,0q�5�   !   $
��Left=�5�   !   $	��Top=�5�   !   $��Tipo de Tesis��5�   !   {�5�   !   �� *
IsEnable+ �� Mode$ TwoWay=�.��5�   "   � .+�5�   &   	� -   5         	�Name$ 	RbtJurisp��5�   )   $	��113��5�   )   $��28��5�   )   $��0,7,0,0q�5�   )   $
��Left=�5�   )   $	��Top=�5�   )   $�� Jurisprudencia��5�   )   {�.  5�   )   	� 5�   &   -   5        $ 
RbtAislada��5�   )   $��80��5�   )   $��28��5�   )   $��	139,7,0,0q�5�   )   $
��Left=�5�   )   $	��Top=�5�   )   $�� T. Aislada��5�   )   {�.  5�   )   5�   #   5�      �� 5�      $	��270��5�   !   $	��240��5�   !   $��
10,489,0,0q�5�   !   $
��Left=�5�   !   $	��Top=�5�   !   $��Materias��5�   !   .��5�   "   � .+�5�   &   �� -   5         ��Name$ TxtMat1��5�   )   $	��200��5�   )   $��25��5�   )   $��
15,25,15,0q�5�   )   $
��Left=�5�   )   $	��Top=�5�   )    �� 
IsReadOnly . ��5�   )   �� *
Materia1+ �� 	Converter# [ �� 5�   &   -   5        $ TxtMat2��5�   )   $	��200��5�   )   $��25��5�   )   $��
15,75,15,0q�5�   )   $
��Left=�5�   )   $	��Top=�5�   )    . ��5�   )   �� *
Materia2+# [ �� 5�   &   -   5        $ TxtMat3��5�   )   $	��200��5�   )   $��25��5�   )   $��15,125,15,0q�5�   )   $
��Left=�5�   )   $	��Top=�5�   )    . ��5�   )   �� *
Materia3+# [ 5�   #   5�      �� 5�      -   5        $ TxtIus��5�   !   $��23��5�   !   $��148,13,10,0q�5�   !   $��Stretch=�5�   !   $	��Top=�5�   !    .  �� TextWrapping$
 Wrap=�5�   !   ��5�   !   �� *Ius+5�   !   �� 5�      -   5        $ TxtEpoca��5�   !   $��23��5�   !   $��148,110,10,0q�5�   !   $��Stretch=�5�   !   $	��Top=�5�   !    . $
 Wrap=�5�   !   ��5�   !   �� *
EpocaInt+# [  5�   !   �� 5�      -   5        $ TxtTesis��5�   !   $��23��5�   !   $��148,67,10,0q�5�   !   $��Stretch=�5�   !   $	��Top=�5�   !    . $
 Wrap=�5�   !   ��5�   !   �� *Tesis+5�   !   �� 5�      $��
10,150,0,0q�5�   !   $
��Left=�5�   !   $	��Top=�5�   !   $��Volumen:��5�   !   �� 5�      -   5        $ 
TxtVolumen��5�   !   $��23��5�   !   $��148,150,10,0q�5�   !   $��Stretch=�5�   !   $	��Top=�5�   !    . $
 Wrap=�5�   !   ��5�   !   �� *	Volumen+5�   !   �� 5�      $��
10,194,0,0q�5�   !   $
��Left=�5�   !   $	��Top=�5�   !   $��Página:��5�   !   �� 5�      -   5        $ 	TxtPagina��5�   !   $	��120��5�   !   $��23��5�   !   $��148,194,0,0q�5�   !   $
��Left=�5�   !   $	��Top=�5�   !    . $
 Wrap=�5�   !   ��5�   !   �� *Pagina+5�   !   �� 5�      $��
11,337,0,0q�5   !   $
��Left=�5  !   $	��Top=�5  !   $��
Instancia:��5  !   �� 5     -   5        $ TxtInstancia��5  !   $	��257��5  !   $��23��5  !   $��
11,368,0,0q�5  !   $
��Left=�5	  !   $	��Top=�5
  !    . $
 Wrap=�5  !   ��5  !   �� *Sala+# [ 5  !   �� 5     $��
10,411,0,0q�5  !   $
��Left=�5  !   $	��Top=�5  !   $��Fuente:��5  !   �� 5     -   5        $ 	TxtFuente��5  !   $	��257��5  !   $��23��5  !   $��
10,442,0,0q�5  !   $
��Left=�5  !   $	��Top=�5  !    . $
 Wrap=�5  !   ��5  !   �� *Fuente+# [ 5  !   5     5     �� 5      $��Otros Textos��6   .��5!     � ���	#FFE5E5E5.+�5"     �� $��	10,10,0,0q�5#  !   $
��Left=�5$  !   $	��Top=�5%  !   $��Genealogía:��5&  !   �� 5'     -   5        $ TxtGenealogia��5(  !   $	��270��5)  !   $	��151��5*  !   $��	10,36,0,0q�5+  !   $
��Left=�5,  !   $	��Top=�5-  !    �� AcceptsReturn . $ WrapWithOverflow=�51  !   " �� VerticalScrollBarVisibility$
 Auto=�52  !    5/  !   �� *
IsReadOnly+$ TwoWay=���50  !   �� *
Genealogia+$ TwoWay=�52  !   �� 53     $��
10,208,0,0q�54  !   $
��Left=�55  !   $	��Top=�56  !   $��Observaciones:��57  !   �� 58     -   5        $ TxtObservaciones��59  !   $	��270��5:  !   $	��146��5;  !   $��
10,234,0,0q�5<  !   $
��Left=�5=  !   $	��Top=�5>  !    . $ WrapWithOverflow=�5B  !   $
 Auto=�5C  !    5@  !   �� *
IsReadOnly+$ TwoWay=���5A  !   �� *Observaciones+$ TwoWay=�5C  !   �� 5D     $��
10,403,0,0q�5E  !   $
��Left=�5F  !   $	��Top=�5G  !   $��Concordancia:��5H  !   �� 5I     -   5        $ TxtConcordancia��5J  !   $	��270��5K  !   $	��166��5L  !   $��
10,428,0,0q�5M  !   $
��Left=�5N  !   $	��Top=�5O  !    . $ WrapWithOverflow=�5S  !   $
 Auto=�5T  !    5Q  !   �� *
IsReadOnly+$ TwoWay=���5R  !   �� *Concordancia+$ TwoWay=�5T  !   5U     5V     5X     5Z     � 5\     $��1��6   .+�5^     ,� -   5         ,�Name$ flowDocViewer��5_     $��0,0,0,40q�5`     ���	#FF414141 ,� Zoom$	 130Q�5b     .��5c     /� -   5         /�Name$ flowDoc��5d      /� 
FontFamily$ Arial)�5e      /� 
FontWeight$ Normal"�5f      /� 
Foreground
 �   �5g     5h     5y     5}     �� 5~  
   $��2��6   5�     5�     