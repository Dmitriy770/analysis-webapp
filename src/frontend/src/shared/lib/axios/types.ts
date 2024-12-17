import type { AxiosResponse, InternalAxiosRequestConfig } from 'axios'

export type AxiosRequestInterceptor = (
  config: InternalAxiosRequestConfig,
) => Promise<InternalAxiosRequestConfig> | InternalAxiosRequestConfig

export type AxiosResponseInterceptor = (response: AxiosResponse) => Promise<AxiosResponse> | AxiosResponse
