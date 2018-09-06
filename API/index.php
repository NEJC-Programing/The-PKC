<?php

$postdata =  file_get_contents("php://input");

$data = json_decode($postdata);

$cmd = $data->cmd;
$api_ver = $data->ver;


