<?php
require 'simple_html_dom.php';
$city = $_GET["city"];
$html = file_get_html("https://koponyeg.hu/elorejelzes/".HuToEn($city));

$temperatures = array();
foreach($html->find('div[class=temp-max] div div') as $element) {
	$temperatures['max'][] = intval($element->plaintext);
}
foreach($html->find('div[class=temp-min] div div') as $element) {
	$temperatures['min'][] = intval($element->plaintext);
}
for($i = 0; $i < count($temperatures['max']); $i++) {
	$d = strtotime("+$i Days");
	$temperatures['date'][] = date("m.d.", $d);
}

echo json_encode($temperatures);

function HuToEn($s)
{
 $hu = array('/é/','/É/','/á/','/Á/','/ó/','/Ó/','/ö/','/Ö/','/ő/','/Ő/','/ú/','/Ú/','/ű/','/Ű/','/ü/','/Ü/','/í/','/Í/','/ /');
 $en = array('e','E','a','A','o','O','o','O','o','O','u','U','u','U','u','U','i','I','_');
 $r = preg_replace($hu,$en,$s);
 return $r;
}
?>