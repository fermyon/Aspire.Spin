use serde::Serialize;
use spin_sdk::http::conversions::IntoBody;
use spin_sdk::http::{IntoResponse, Request, Response};
use spin_sdk::http_component;

#[derive(Serialize)]
pub struct ResponseModel {
    pub message: String,
}

impl IntoBody for ResponseModel {
    fn into_body(self) -> Vec<u8> {
        serde_json::to_vec(&self).unwrap()
    }
}

#[http_component]
fn api_one(req: Request) -> anyhow::Result<impl IntoResponse> {
    println!("{}: {:?}", req.method(), req.header("spin-full-url"));
    let res = ResponseModel {
        message: String::from("this is API 1"),
    };

    Ok(Response::builder()
        .status(200)
        .header("content-type", "application/json")
        .body(res.into_body())
        .build())
}
